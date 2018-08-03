using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class ManageSerieController : BaseController
    {
        // GET: ManageSerie
        public ActionResult Index()
        {

            VideoOnDemandContext context = new VideoOnDemandContext();
            SerieRepository repository = new SerieRepository(context);

            // Consultar las series
            var lst = repository.GetAll();

            // Mapear la lista de series con una lista de SerieViewModel
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);

            return View(models);
        }

        // GET: ManageSerie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageSerie/Create
        public ActionResult Create()
        {
            var model = new SerieViewModel();
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.GetAll();
            var lst2 = personaRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);
            return View(model);
        }

        // POST: ManageSerie/Create
        [HttpPost]
        public ActionResult Create(SerieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lstGeneros = generoRepository.GetAll();
            var lstPersonas = personaRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lstGeneros);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lstPersonas);

            try
            {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    SerieRepository repository = new SerieRepository(context);
                    Serie serie = MapHelper.Map<Serie>(model);
                    repository.InsertComplete(serie, model.GenerosSeleccionados, model.PersonasSeleccionadas);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                   }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageSerie/Edit/5
        public ActionResult Edit(int id)
        {
            SerieRepository serieRepository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);

            var serie = serieRepository.Query(t => t.id == id).First();
            var lstGeneros = generoRepository.GetAll();
            var lstPersonas = personaRepository.GetAll();

            var model = MapHelper.Map<SerieViewModel>(serie);

            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lstGeneros);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lstPersonas);

            return View(model);
        }

        // POST: ManageSerie/Edit/5
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

        // GET: ManageSerie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageSerie/Delete/5
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

        public ActionResult Episodios(int id)
        {
            return RedirectToRoute("Index", "ManageEpisodio");
        }

    }
}
