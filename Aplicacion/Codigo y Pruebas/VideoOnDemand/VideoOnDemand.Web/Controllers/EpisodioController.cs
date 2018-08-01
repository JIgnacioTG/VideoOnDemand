using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoOnDemand.Web.Controllers
{
    public class EpisodioController : BaseController
    {
        // GET: Episodio
        public ActionResult Index()
        {
            return View();
        }

        // GET: Episodio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Episodio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Episodio/Create
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

        // GET: Episodio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Episodio/Edit/5
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

        // GET: Episodio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Episodio/Delete/5
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
