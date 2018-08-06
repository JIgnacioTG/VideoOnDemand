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

        public ActionResult Index(int? idg, string nombre = "")
        {

            //paginacion
            int totalPages = 0;
            int totalRows = 0;
            int pageSize = 40;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);

            MovieRepository repository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.GetAll();

            Expression<Func<Movie, bool>> expr = m => m.nombre.Contains(nombre);

            if (idg != null)
            {
                Genero genFind = genero.FirstOrDefault(m => m.Id == idg);
                
                expr = m => m.Generos.Any(x => x.Id == idg);
            }
            
            var lst = repository.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);
            
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            var generos = MapHelper.Map<ICollection<GeneroViewModel>>(generoRepository.GetAll());
            if (models.Count() != 0)
            {
                models.First().GenerosDisponibles = generos;
            }

            var model = new PaginatorViewModel<MovieViewModel>
            {
                Page = page,
                TotalPages = totalPages,
                TotalRows = totalRows,
                PageSize = pageSize,
                Results = models
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var topic = repository.Query(t => t.id == id).First();

            var model = MapHelper.Map<MovieViewModel>(topic);

            return View(model);
        }
    }
}