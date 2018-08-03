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
    [RoutePrefix("ManageEpisodio")]
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageEpisodio/Create
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
    }
}
