using AppFramework.Expressions;
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

            ViewBag.Resenias = resenias;
            ViewBag.CountResenias = count;
            return View(model);
        }

        public ActionResult MyList()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddResenia(OpinionViewModel model)
        {
            UsuarioRepository userRepo = new UsuarioRepository(context);
            MediaRepository mediaRepo = new MediaRepository(context);


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


            OpinionRepository opinionRepo = new OpinionRepository(context);
                
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
            if (ModelState.IsValid)
            {
                

                return Json(new
                {
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new
                {
                    Success = false
                }, JsonRequestBehavior.AllowGet);
        }

    }
}