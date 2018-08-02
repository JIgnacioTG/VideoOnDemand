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
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);

                    #region Validaciones
                    //Nombre Unico
                    var generoName = new Genero { Nombre = model.Nombre };

                    bool existeGenero = repository.QueryByExample(generoName).Count > 0;

                    if (existeGenero)
                    {
                        ModelState.AddModelError("Nombre", "El Nombre del género ya existe");
                        return View(model);
                    }

                    #endregion

                    Genero genero = MapHelper.Map<Genero>(model);

                    repository.Insert(genero);

                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
        public ActionResult Edit(int id, GeneroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);

                    var genero = MapHelper.Map<Genero>(model);

                    repository.Delete(genero);

                    context.SaveChanges();
                }

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
