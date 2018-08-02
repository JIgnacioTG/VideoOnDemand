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
    public class GeneroController : BaseController
    {
        // GET: Genero
        public ActionResult Index()
        {
            GeneroRepository repository = new GeneroRepository(context);
            //consulte los cursos del repositorio
            var lst = repository.GetAll();
            //mapeamos la lista de cursos con una lista de cursos view model
            var models = MapHelper.Map<IEnumerable<GeneroViewModel>>(lst);
            return View(models);
        }

        // GET: Genero/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        public ActionResult Create(GeneroViewModel model)
        {
            try
            {
                GeneroRepository repository = new GeneroRepository(context);
                #region Validaciones
                var nombreGenero = new Genero { Nombre = model.Nombre };

                bool existeGenero = repository.QueryByExample(nombreGenero).Count>0;

                if (existeGenero)
                {
                    ModelState.AddModelError("Nombre", "El nombre de género ya existe");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Genero/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Genero/Edit/5
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

        // GET: Genero/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Genero/Delete/5
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
