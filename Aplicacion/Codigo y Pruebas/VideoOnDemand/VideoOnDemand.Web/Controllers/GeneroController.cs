using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public struct lstMedias
        {
            public int? id;
            public string nombre;
            public string tipo;
        }
        
        // GET: Genero
        public ActionResult Index()
        {
            GeneroRepository repository = new GeneroRepository(context);
            //consulte los cursos del repositorio
            var lst = repository.Query(g => g.Eliminado != true, "Nombre");
            //mapeamos la lista de cursos con una lista de cursos view model
            var models = MapHelper.Map<IEnumerable<GeneroViewModel>>(lst);
            return View(models);
        }

        // GET: Genero/Details/5
        
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
                    genero.Eliminado = false;
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
            GeneroRepository repository = new GeneroRepository(context);
            var genero = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<GeneroViewModel>(genero);

            return View(model);
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

                    repository.Update(genero);

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
        public ActionResult Delete(int? id)
        {
            GeneroRepository repository = new GeneroRepository(context);
            var genero = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<GeneroViewModel>(genero);
            if(ViewBag.Error == 1)
            {
                ViewBag.Salto = 1;
            } else
            {
                ViewBag.Salto = 0;
            }
            return View(model);
        }

        // POST: Genero/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, GeneroViewModel model)
        {
            try
            {
                GeneroRepository repo = new GeneroRepository(context);
                

                var genero = repo.Query(g => g.Id == id).FirstOrDefault();
                context.Entry(genero).Collection(g => g.Medias).Load();

                if (genero.Medias.Count() == 0)
                {
                    repo.LogicalDelete(genero);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    List<lstMedias> lista = new List<lstMedias>();
                    var repository = new MovieRepository(context);
                    var repository2 = new SerieRepository(context);

                    foreach (var media in genero.Medias)
                    {
                        var mov = repository.Query(m => m.id == media.id);
                        var ser = repository2.Query(s => s.id == media.id);

                        if (mov.Count() == 1)
                        {
                            lista.Add(new lstMedias() { id = media.id, nombre = media.nombre, tipo = "Película" });
                        }
                        else if (ser.Count() == 1)
                        {
                            lista.Add(new lstMedias() { id = media.id, nombre = media.nombre, tipo = "Serie" });
                        }

                    }
                    ViewData["inUsing"] = lista;
                    ViewBag.Error = 1;
                    model = MapHelper.Map<GeneroViewModel>(genero);
                    return Delete(genero.Id);
                }
            }
            catch
            {
                return View();
            }
        }


    }
}
