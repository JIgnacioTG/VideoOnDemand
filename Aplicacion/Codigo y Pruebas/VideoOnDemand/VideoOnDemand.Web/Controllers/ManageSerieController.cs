using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
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
            return View();
        }

        // POST: ManageSerie/Create
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

        // GET: ManageSerie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
    }
}
