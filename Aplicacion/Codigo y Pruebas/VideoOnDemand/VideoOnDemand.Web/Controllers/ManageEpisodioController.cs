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

            ViewBag.Serie = serieRepository.Query(s => s.id == id).FirstOrDefault().nombre;
            ViewBag.SerieId = serieRepository.Query(s => s.id == id).FirstOrDefault().id;

            return View(models);
        }

        // GET: ManageEpisodio/Create
        public ActionResult Create(int id)
        {
            var model = new EpisodioViewModel();

            SerieRepository serieRepository = new SerieRepository(context);

            var serie = serieRepository.Query(s => s.id == id).FirstOrDefault();
            model.Serie = MapHelper.Map<SerieViewModel>(serie);
            model.serieId = id;

            model.estado = EEstatusMedia.VISIBLE;

            ViewBag.Serie = serie.nombre;

            return View(model);
        }

        // POST: ManageEpisodio/Create
        [HttpPost]
        public ActionResult Create(EpisodioViewModel model)
        {

            try
            {
                // TODO: Add insert logic here
                EpisodioRepository episodioRepository = new EpisodioRepository(context);
                SerieRepository serieRepository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = serieRepository.Query(s => s.id == model.id).First();
                    var episodio = MapHelper.Map<Episodio>(model);

                    var lstEpisodios = episodioRepository.Query(e => e.serieId == model.id);

                    context.Entry(serie).Collection(s => s.Generos).Load();
                    context.Entry(serie).Collection(s => s.Actores).Load();

                    episodio.Serie = serie;
                    episodio.serieId = model.id;
                    episodio.Actores = serie.Actores;
                    episodio.Generos = serie.Generos;
                    episodio.fechaRegistro = DateTime.Now;

                    ViewBag.Serie = serie.nombre;

                    #region Validaciones
                    foreach (var e in lstEpisodios)
                    {
                        if (e.nombre.ToLower() == episodio.nombre.ToLower())
                        {
                            if (e.fechaLanzamiento == episodio.fechaLanzamiento)
                            {
                                ViewBag.Error = 1;
                                return Create(model.id);
                            }
                        }
                        if (e.temporada == episodio.temporada)
                        {
                            if (e.numEpisodio == episodio.numEpisodio)
                            {
                                ViewBag.Error = 2;
                                return Create(model.id);
                            }
                        }
                    }
                    #endregion

                    episodioRepository.Insert(episodio);

                    context.SaveChanges();

                    return RedirectToAction("Index", new { id = episodio.serieId });
                }

                return Create(model.id);
            }
            catch
            {
                return Create(model.id);
            }
        }

        // GET: ManageEpisodio/Edit/5
        public ActionResult Edit(int id)
        {
            EpisodioRepository episodioRepository = new EpisodioRepository(context);

            var episodio = episodioRepository.Query(e => e.id == id).First();

            var model = MapHelper.Map<EpisodioViewModel>(episodio);

            context.Entry(episodio).Reference(e => e.Serie).Load();

            ViewBag.Serie = episodio.Serie.nombre;

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

                    var lstEpisodios = episodioRepository.Query(e => e.serieId == episodio.serieId);

                    ViewBag.Serie = episodio.Serie.nombre;

                    #region Validaciones
                    foreach (var e in lstEpisodios)
                    {
                        if (e.id != id)
                        {
                            if (e.nombre.ToLower() == episodio.nombre.ToLower())
                            {
                                if (e.fechaLanzamiento == episodio.fechaLanzamiento)
                                {
                                    ViewBag.Error = 1;
                                    return Edit(id);
                                }
                            }
                        }
                        if (e.temporada == episodio.temporada)
                        {
                            if (e.numEpisodio == episodio.numEpisodio)
                            {
                                ViewBag.Error = 2;
                                return Edit(id);
                            }
                        }
                    }
                    #endregion

                    context.Entry(episodio).Collection(e => e.Actores).Load();
                    context.Entry(episodio).Collection(e => e.Generos).Load();
                    context.Entry(episodio).Collection(e => e.Opiniones).Load();
                    context.Entry(episodio).Reference(e => e.Serie).Load();

                    episodioRepository.Update(episodio);
                    context.SaveChanges();
                    return RedirectToAction("Index", new { id = episodio.serieId });
                }

                return Edit(id);

            }
            catch
            {
                return Edit(id);
            }
        }

        // GET: ManageEpisodio/Delete/5
        public ActionResult Delete(int id)
        {
            EpisodioRepository episodioRepository = new EpisodioRepository(context);

            var episodio = episodioRepository.Query(s => s.id == id).First();

            context.Entry(episodio).Reference(e => e.Serie).Load();

            ViewBag.Serie = episodio.Serie.nombre;

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

                context.Entry(episodio).Reference(e => e.Serie).Load();

                ViewBag.Serie = episodio.Serie.nombre;

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
