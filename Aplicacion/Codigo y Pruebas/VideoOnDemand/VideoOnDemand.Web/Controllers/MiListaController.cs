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

            MovieRepository movieRepository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            UsuarioRepository userRepo = new UsuarioRepository(context);
            FavoritoRepository favRepo = new FavoritoRepository(context);

            var yo = userRepo.Query(u => u.IdentityId == UserId).FirstOrDefault();
            var misFavoritos = favRepo.Query(f => f.usuarioId == yo.Id);

            var genero = generoRepository.GetAll();
            
            Expression<Func<Movie, bool>> expr = m => m.estado == EEstatusMedia.VISIBLE &&  m.nombre.Contains(nombre);

            if (idg != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idg));
            }

            foreach (var item in misFavoritos)
            {
                expr = expr.And(m => m.id == item.mediaId);
            }



            

            var lst = movieRepository.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);

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
            ViewBag.UserId = UserId;

            return View(model);
        }
    }
}