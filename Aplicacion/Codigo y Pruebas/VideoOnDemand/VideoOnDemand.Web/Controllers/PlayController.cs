using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoOnDemand.Web.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        public ActionResult Index()
        {
            return View();
        }

        // GET: Play/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Play/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Play/Create
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

        // GET: Play/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Play/Edit/5
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

        // GET: Play/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Play/Delete/5
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
