using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoOnDemand.Web.Controllers
{
    public class ManageEpisodioController : BaseController
    {
        // GET: ManageEpisodio
        public ActionResult Index()
        {
            return View();
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
