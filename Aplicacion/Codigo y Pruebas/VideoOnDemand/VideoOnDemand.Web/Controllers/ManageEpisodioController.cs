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
            var lstEpisodio = episodioRepository.Query(e => e.serieId.Value == id && e.estado != EEstatusMedia.ELIMINADO).OrderBy(e => e.numEpisodio).OrderBy(e => e.temporada);

            // Mapear la lista de series con una lista de SerieViewModel
            var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lstEpisodio);

            if (lstEpisodio.Count() == 0)
            {
                List<Episodio> lstEpisodioVacio = new List<Episodio> { new Episodio { Serie = serieRepository.Query(s => s.id == id).First() } };
                models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lstEpisodioVacio);
            }

            return View(models);
        }

        // GET: ManageEpisodio/Create
        public ActionResult Create(int id)
        {
            var model = new EpisodioViewModel();

            SerieRepository serieRepository = new SerieRepository(context);

            var serie = serieRepository.Query(s => s.id == id).First();
            model.Serie = MapHelper.Map<SerieViewModel>(serie);
            model.serieId = id;

            model.estado = EEstatusMedia.VISIBLE;

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
                    var serie = serieRepository.Query(s => s.id == model.id).First();
                    var episodio = MapHelper.Map<Episodio>(model);
                    episodio.Serie = serie;
                    episodio.serieId = model.id;
                    episodioRepository.InsertComplete(episodio, model.GenerosSeleccionados, model.PersonasSeleccionadas);

                    context.SaveChanges();

                    return RedirectToAction("Index", new { id = episodio.serieId });
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
            EpisodioRepository episodioRepository = new EpisodioRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);

            var episodio = episodioRepository.Query(e => e.id == id).First();
            var lstGeneros = generoRepository.GetAll();
            var lstPersonas = personaRepository.GetAll();

            var model = MapHelper.Map<EpisodioViewModel>(episodio);

            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lstGeneros);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lstPersonas);
            //model.Serie = MapHelper.Map<SerieViewModel>(episodio.Serie);

            return View(model);
        }

        // POST: ManageEpisodio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EpisodioViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                EpisodioRepository episodioRepository = new EpisodioRepository(context);

                if (ModelState.IsValid)
                {
                    var episodio = episodioRepository.Query(e => e.id == id).First();
                    episodio = Update(episodio, model);
                    if (episodio.Serie.estado != EEstatusMedia.INVISIBLE)
                    {
                        episodioRepository.UpdateComplete(episodio, model.GenerosSeleccionados, model.PersonasSeleccionadas);
                        context.SaveChanges();
                        return RedirectToAction("Index", new { id = episodio.serieId });
                    }
                    else
                        return View(model);
                }

                return View(model);

            }
            catch
            {
                return View(model);
            }
        }

        // GET: ManageEpisodio/Delete/5
        public ActionResult Delete(int id)
        {
            EpisodioRepository episodioRepository = new EpisodioRepository(context);

            var episodio = episodioRepository.Query(s => s.id == id).First();

            var model = MapHelper.Map<EpisodioViewModel>(episodio);

            return View(model);
        }

        // POST: ManageEpisodio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EpisodioViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                EpisodioRepository episodioRepository = new EpisodioRepository(context);

                var episodio = episodioRepository.Query(s => s.id == id).First();

                episodioRepository.LogicalDelete(episodio);
                context.SaveChanges();
                return RedirectToAction("Index", new { id = episodio.serieId });
            }
            catch
            {
                return View(model);
            }
        }

        public Episodio Update(Episodio episodio, EpisodioViewModel model)
        {
            episodio.numEpisodio = model.numEpisodio;
            episodio.temporada = model.temporada;
            episodio.descripcion = model.descripcion;
            episodio.duracionMin = model.duracionMin;
            episodio.estado = model.estado;
            episodio.fechaLanzamiento = model.fechaLanzamiento;
            episodio.nombre = model.nombre;
            return episodio;
        }

    }
}
