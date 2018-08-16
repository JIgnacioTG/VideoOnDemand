using AppFramework.Expressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class MovieController : BaseController
    {

        public SelectList GeneroList(object selectecItem = null)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(null, "Nombre").ToList();
            genero.Insert(0, new Genero { Id = null, Nombre = "Seleccione" });
            return new SelectList(genero, "Id", "Nombre", selectecItem);
        }

        public ActionResult Index(int? idg, string nombre = "", int paginado = 40)
        {
            if(paginado <= 0)
            {
                paginado = 40;
            }
            

            //paginacion
            int totalPages = 0;
            int totalRows = 0;
            int pageSize = paginado;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);

            MovieRepository repository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.GetAll();

            Expression<Func<Movie, bool>> expr = m => m.estado == EEstatusMedia.VISIBLE && m.nombre.Contains(nombre);

            if (idg != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idg));
            }
               
            var lst = repository.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);
            
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            var generos = MapHelper.Map<ICollection<GeneroViewModel>>(generoRepository.GetAll());

            var model = new PaginatorViewModel<MovieViewModel>
            {
                Page = page,
                TotalPages = totalPages,
                TotalRows = totalRows,
                PageSize = pageSize,
                Results = models
            };

            ViewBag.ListaGenero = GeneroList(genero);
            ViewBag.Nombre = nombre;
            ViewBag.Idg = idg + "";
            ViewBag.Paginado = paginado + "";

            return View(model);
        }

        [HttpGet]
        public ActionResult getResenias(int MediaId, string UserId)
        {
            OpinionRepository opiRepo = new OpinionRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);

            int numResTotales = opiRepo.Query(x => x.MediaId == MediaId).Count();
            var usuario = userRepo.GetAll().FirstOrDefault(u => u.IdentityId == UserId);

            var opinionesMedia = from o in opiRepo.GetAll()
                                 where o.MediaId == MediaId
                                 && o.UsuarioId != usuario.Id
                                 select o;

            foreach (var opinion in opinionesMedia)
            {
                opinion.Usuario = userRepo.GetAll().FirstOrDefault(u => u.Id == opinion.UsuarioId);
            }

            return Json(new
            {
                Success = true,
                TotalResenias = numResTotales,
                Opiniones = JsonConvert.SerializeObject(opinionesMedia)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getMiResenia(int MediaId, string UserId)
        {
            OpinionRepository opinionRepo = new OpinionRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);

            int numResTotales = opinionRepo.Query(x => x.MediaId == MediaId).Count();
            var usuario = userRepo.GetAll().FirstOrDefault(u => u.IdentityId == UserId);

            if (usuario == null)
            {
                return Json(new
                {
                    Success = false,
                    TotalResenias = numResTotales
                }, JsonRequestBehavior.AllowGet);
            }

            //Obtener resenia de un usuario
            var miResenia = opinionRepo.GetAll().FirstOrDefault(o => o.MediaId == MediaId && o.UsuarioId == usuario.Id);
            

            if (miResenia == null)
            {

                return Json(new
                {
                    Success = false,
                    TotalResenias = numResTotales
                }, JsonRequestBehavior.AllowGet);
            }
            miResenia.Usuario = usuario;
            return Json(new
            {
                Success = true,
                TotalResenias = numResTotales,
                Opinion = JsonConvert.SerializeObject(miResenia)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            OpinionRepository reseniaRepository = new OpinionRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);

            var movie = repository.Query(t => t.id == id).First();
            var opiniones = reseniaRepository.GetAll();
            var resenias = from o in opiniones
                           where o.Media.id == movie.id
                           select o;

            var model = MapHelper.Map<MovieViewModel>(movie);
            int count = 0;
            foreach (var item in resenias)
            {
                item.Usuario = userRepo.GetAll().FirstOrDefault(u => u.Id == item.UsuarioId);
                count++;
            }

            ViewBag.CountResenias = count;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddResenia(OpinionViewModel model)
        {
            UsuarioRepository userRepo = new UsuarioRepository(context);
            MediaRepository mediaRepo = new MediaRepository(context);
            OpinionRepository opinionRepo = new OpinionRepository(context);

            model.FechaRegistro = DateTime.Now;
            model.Usuario = (from u in userRepo.GetAll()
                              where u.IdentityId == model.IdentityId
                              select u).FirstOrDefault();
            model.Media = (from m in mediaRepo.GetAll()
                           where m.id == model.MediaId
                           select m).FirstOrDefault();
            model.UsuarioId = (from u in userRepo.GetAll()
                             where u.IdentityId == model.IdentityId
                             select u.Id).FirstOrDefault();

            #region Validaciones
            //Una Reseña por Usuario
            var userExist = opinionRepo.GetAll().FirstOrDefault(u => u.UsuarioId == model.UsuarioId && u.MediaId == model.MediaId);

            if(userExist != null)
            {
                
                return Json(new
                {
                    Success = false,
                    TypeError = 1
                }, JsonRequestBehavior.AllowGet);
            }

            //Descripcion requerida
             if(model.Descripcion == null)
            {
                return Json(new
                {
                    Success = false,
                    TypeError = 2
                }, JsonRequestBehavior.AllowGet);
            }

             // Descripción mayor de 200
            if (model.Descripcion.FirstOrDefault().ToString().Length > 200)
            {
                return Json(new
                {
                    Success = false,
                    TypeError = 3
                }, JsonRequestBehavior.AllowGet);
            }

            //Puntuacion Requerida - Si no califica se vuelve 0
            if (model.Puntuacion == null)
            {
                model.Puntuacion = 0;
            }
            #endregion

            var opinion = MapHelper.Map<Opinion>(model);
                opinionRepo.Insert(opinion);

            context.SaveChanges();

            return Json(new
            {
                Success = true
            }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult AddLista(FavoritoViewModel model)
        {
            FavoritoRepository favRepo = new FavoritoRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);
            model.FechaAgregado = DateTime.Now;
            model.usuarioId = (from u in userRepo.GetAll()
                              where u.IdentityId == model.UserID
                              select u.Id).FirstOrDefault();

            #region Validaciones
            //Que Exista Ya en tu lista
            var existFav = favRepo.GetAll().FirstOrDefault(f => f.mediaId == model.mediaId && f.usuarioId == model.usuarioId);

            if(existFav != null)
            {
                return Json(new
                {
                    Success = false,
                    TypeError = 1
                }, JsonRequestBehavior.AllowGet);
            }
            #endregion
            var favorito = MapHelper.Map<Favorito>(model);

            favRepo.Insert(favorito);

            context.SaveChanges();
            return Json(new
            {
                Success = true

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EliminarOpinion(int MediaId, string UserId)
        {
            OpinionRepository opiRepo = new OpinionRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);

            var usuario = userRepo.Query(u => u.IdentityId == UserId).FirstOrDefault();

            if (usuario == null)
            {
                return Json(new
                {
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }

            var opinion = opiRepo.Query(o => o.MediaId == MediaId && o.UsuarioId == usuario.Id).FirstOrDefault();

            opiRepo.Delete(opinion);
            context.SaveChanges();
            return Json(new
            {
                Success = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}