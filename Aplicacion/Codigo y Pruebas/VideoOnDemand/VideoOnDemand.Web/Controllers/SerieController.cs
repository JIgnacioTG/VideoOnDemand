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
    public class SerieController : BaseController
    {
       
        // GET: Serie
        public ActionResult Index(int? idg, string nombre = "")
        {
            int totalPages = 0;
            int totalRows = 0;
            int pageSize = 40;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);


            SerieRepository repository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.GetAll();
            Expression<Func<Serie, bool>> expr = m => m.nombre.Contains(nombre);

            if (idg != null)
            {
                Genero genFind = genero.FirstOrDefault(m => m.Id == idg);

                expr = m => m.Generos.Any(x => x.Id == idg);
            }

            var lst = repository.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);

            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
            var generos = MapHelper.Map<ICollection<GeneroViewModel>>(generoRepository.GetAll());
            if (models.Count() != 0)
            {
                models.First().GenerosDisponibles = generos;
            }

            var model = new PaginatorViewModel<SerieViewModel>
            {
                Page = page,
                TotalPages = totalPages,
                TotalRows = totalRows,
                PageSize = pageSize,
                Results = models
            };

            return View(model);
        }

        // GET: Serie/Details/5
        public ActionResult Details(int id)
        {
            SerieRepository repository = new SerieRepository(context);
            var topic = repository.Query(t => t.id == id).First();

            var model = MapHelper.Map<SerieViewModel>(topic);

            return View(model);
        }

        // GET: Serie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Serie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Serie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Serie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Serie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Serie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
