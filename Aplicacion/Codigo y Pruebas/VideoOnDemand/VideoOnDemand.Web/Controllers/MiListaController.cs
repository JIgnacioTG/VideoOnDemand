using AppFramework.Expressions;
using Microsoft.AspNet.Identity;
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
    public class MiListaController : BaseController
    {
        public SelectList GeneroList(object selectecItem = null)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(null, "Nombre").ToList();
            genero.Insert(0, new Genero { Id = null, Nombre = "Seleccione" });
            return new SelectList(genero, "Id", "Nombre", selectecItem);
        }

        public ActionResult Index(int? idg, string nombre = "", int paginado = 40, string UserId = "")
        {
            
            if (paginado <= 0)
            {
                paginado = 40;
            }


            //paginacion
            int totalPages = 0;
            int totalRows = 0;
            int pageSize = paginado;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);

            MediaRepository mediaRepo = new MediaRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);
            FavoritoRepository favRepo = new FavoritoRepository(context);

            var yo = userRepo.Query(u => u.IdentityId == UserId).FirstOrDefault();

            var misFavoritos = favRepo.Query(f => f.usuarioId == yo.Id);

            var genero = generoRepository.GetAll();

            Expression<Func<Media, bool>> expr = m => m.estado == EEstatusMedia.VISIBLE ;

            int count = 0;
            foreach (var item in misFavoritos)
            {
                if (count == 0)
                {
                    expr = expr.And(m => m.id == item.mediaId);
                    count++;
                }
                else
                    expr = expr.Or(m => m.id == item.mediaId);
            }

            if (idg != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idg));
            }

            expr = expr.And(m => m.estado == EEstatusMedia.VISIBLE && m.nombre.Contains(nombre));

            var lst = mediaRepo.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);

            var models = MapHelper.Map<IEnumerable<MediaViewModel>>(lst);

            var model = new PaginatorViewModel<MediaViewModel>
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
            ViewBag.UserId = UserId;
            ViewBag.numList = misFavoritos.Count();

            return View(model);
        }

        [HttpGet]
        public ActionResult eliminarDeMiLista(int MediaId, string UserId)
        {

            UsuarioRepository userRepo = new UsuarioRepository(context);
            FavoritoRepository favRepo = new FavoritoRepository(context);

            //Me Obtengo
            var yo = userRepo.Query(u => u.IdentityId == UserId).FirstOrDefault();
            //Obtengo de favoritos el favorito que contenga mi id y el mediaId
            var eliminar = favRepo.Query(f => f.usuarioId == yo.Id && f.mediaId == MediaId).FirstOrDefault();

            if (eliminar != null)
            {
                favRepo.Delete(eliminar);
                context.SaveChanges();

                return Json(new
                {
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Success = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}