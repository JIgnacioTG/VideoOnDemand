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
    public class SerieController : BaseController
    {

        public SelectList GeneroList(object selectecItem = null)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(null, "Nombre").ToList();
            genero.Insert(0, new Genero { Id = null, Nombre = "Seleccione" });
            return new SelectList(genero, "Id", "Nombre", selectecItem);
        }
        
        // GET: Serie
        public ActionResult Index(int? idg, string nombre = "",int paginado = 40)
        {
            if (paginado <= 0)
            {
                paginado = 40;
            }

            int totalPages = 0;
            int totalRows = 0;
            int pageSize = paginado;
            int page = Request.QueryString["page"] == null ? 1 : int.Parse(Request.QueryString["page"]);


            SerieRepository repository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.GetAll();

            Expression<Func<Serie, bool>> expr = m => m.estado == EEstatusMedia.VISIBLE && m.nombre.Contains(nombre);

            if (idg != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idg));
            }

            var lst = repository.QueryPage(expr, out totalPages, out totalRows, "Nombre", page - 1, pageSize);

            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
            var generos = MapHelper.Map<ICollection<GeneroViewModel>>(generoRepository.GetAll());

            var model = new PaginatorViewModel<SerieViewModel>
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
