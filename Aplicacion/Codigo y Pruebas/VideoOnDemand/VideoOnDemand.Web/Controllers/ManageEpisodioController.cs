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
    public class ManageEpisodioController : BaseController
    {
        // GET: ManageEpisodio
        public ActionResult Index(int id)
        {
            VideoOnDemandContext context = new VideoOnDemandContext();
            EpisodioRepository episodioRepository = new EpisodioRepository(context);
            SerieRepository serieRepository = new SerieRepository(context);

            // Consultar los episodios de la serie asignada
            var lstEpisodio = episodioRepository.Query(e => e.serieId.Value == id);
            var serie = serieRepository.Query(s => s.id == id).FirstOrDefault();

            foreach (var item in lstEpisodio)
            {
                item.Serie = serie;
            }

            // Mapear la lista de series con una lista de SerieViewModel
            var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lstEpisodio);

            return View(models);
        }

        // GET: ManageEpisodio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageEpisodio/Create
        public ActionResult Create(int id)
        {
            var model = new EpisodioViewModel();
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.GetAll();
            var lst2 = personaRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            SerieRepository serieRepository = new SerieRepository(context);

            var serie = serieRepository.Query(s => s.id == id).First();
            model.Serie = MapHelper.Map<SerieViewModel>(serie);
            model.serieId = id;

            return View(model);
        }

        // POST: ManageEpisodio/Create
        [HttpPost]
        public ActionResult Create(EpisodioViewModel model)
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
                EpisodioRepository episodioRepository = new EpisodioRepository(context);
                SerieRepository serieRepository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    model.serieId = model.id;
                    var serie = serieRepository.Query(s => s.id == model.id).First();
                    model.Serie = MapHelper.Map<SerieViewModel>(serie);
                    var episodio = MapHelper.Map<Episodio>(model);
                    episodioRepository.InsertComplete(episodio, model.GenerosSeleccionados, model.PersonasSeleccionadas);

                    context.SaveChanges();

                    return RedirectToAction("Index", new { id = model.serieId });
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ManageEpisodio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManageEpisodio/Edit/5
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

        // GET: ManageEpisodio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageEpisodio/Delete/5
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

        public Episodio Update(Episodio episodio, EpisodioViewModel model)
        {
            episodio.descripcion = model.descripcion;
            episodio.duracionMin = model.duracionMin;
            episodio.estado = model.estado;
            episodio.fechaLanzamiento = model.fechaLanzamiento;
            episodio.nombre = model.nombre;
            return episodio;
        }

    }
}
