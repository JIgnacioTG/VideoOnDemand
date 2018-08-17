using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;
using AppFramework.Expressions;

namespace VideoOnDemand.Web.Controllers
{
    public class ManageSerieController : BaseController
    {
        // GET: ManageSerie
        public ActionResult Index(int? idGenero, string nombre = "")
        {
            SerieRepository serieRepository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.Query(g => g.Eliminado == false, "Nombre");

            Expression<Func<Serie, bool>> expr = m => m.estado != EEstatusMedia.ELIMINADO;
            if (idGenero != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idGenero));

            }
            if (!string.IsNullOrEmpty(nombre))
            {
                expr = expr.And(x => x.nombre.Contains(nombre));
            }
            var lst = serieRepository.Query(expr, "Nombre");
            var model = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
            var lstGenero = generoRepository.Query(g => g.Eliminado == false, "Nombre");

            ViewBag.ListaGenero = GeneroList(lstGenero);

            return View(model);
        }

        // GET: ManageSerie/Create
        public ActionResult Create()
        {
            var model = new SerieViewModel();
            model = LinkLists(model);
            model.estado = EEstatusMedia.VISIBLE;
            return View(model);
        }

        // POST: ManageSerie/Create
        [HttpPost]
        public ActionResult Create(SerieViewModel model)
        {
            model = LinkLists(model);
            model.duracionMin = 0;

            try
            {

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    SerieRepository repository = new SerieRepository(context);
                    Serie serie = MapHelper.Map<Serie>(model);
                    var lstSerie = repository.Query(s => s.estado != EEstatusMedia.ELIMINADO);
                    foreach (var s in lstSerie)
                    {
                        if (s.nombre.ToLower() == serie.nombre.ToLower())
                        {
                            if (s.fechaLanzamiento == serie.fechaLanzamiento)
                            {
                                ViewBag.Error = 1;
                                return Create();
                            }
                        }
                    }
                    repository.InsertComplete(serie, model.GenerosSeleccionados, model.PersonasSeleccionadas);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Create();
                }
            }
            catch
            {
                return Create();
            }
        }

        // GET: ManageSerie/Edit/5
        public ActionResult Edit(int id)
        {
            SerieRepository serieRepository = new SerieRepository(context);

            var serie = serieRepository.Query(t => t.id == id).First();

            var model = MapHelper.Map<SerieViewModel>(serie);

            model = LinkLists(model);

            return View(model);
        }

        // POST: ManageSerie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SerieViewModel model)
        {
            model = LinkLists(model);

            try
            {
                var serieRepository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = serieRepository.Query(s => s.id == id).First();
                    serie = Update(serie, model);
                    var lstSerie = serieRepository.Query(s => s.estado != EEstatusMedia.ELIMINADO);
                    foreach (var s in lstSerie)
                    {
                        if (s.id != id)
                        {
                            if (s.nombre.ToLower() == serie.nombre.ToLower())
                            {
                                if (s.fechaLanzamiento == serie.fechaLanzamiento)
                                {
                                    model = MapHelper.Map<SerieViewModel>(serie);
                                    model = LinkLists(model);
                                    ViewBag.Error = 1;
                                    return Edit(id);
                                }
                            }
                        }
                    }
                    serieRepository.UpdateComplete(serie, model.GenerosSeleccionados, model.PersonasSeleccionadas);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return Edit(id);
            }
            catch
            {
                return Edit(id);
            }
        }

        // GET: ManageSerie/Delete/5
        public ActionResult Delete(int id)
        {
            SerieRepository serieRepository = new SerieRepository(context);

            var serie = serieRepository.Query(s => s.id == id).First();

            var model = MapHelper.Map<SerieViewModel>(serie);

            return View(model);
        }

        // POST: ManageSerie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SerieViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                var serieRepository = new SerieRepository(context);

                var serie = serieRepository.Query(s => s.id == id).First();

                serieRepository.LogicalDelete(serie);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(model);
            }
        }

        public Serie Update(Serie serie, SerieViewModel model)
        {
            serie.descripcion = model.descripcion;
            serie.estado = model.estado;
            serie.fechaLanzamiento = model.fechaLanzamiento;
            serie.nombre = model.nombre;
            return serie;
        }

        public SerieViewModel LinkLists (SerieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lstGeneros = generoRepository.Query(g => g.Eliminado == false, "Nombre");
            var lstPersonas = personaRepository.Query(p => p.Eliminado == false, "Nombre");
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lstGeneros);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lstPersonas);

            return model;
        }

        public SelectList GeneroList(object selectecItem = null)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(g => g.Eliminado == false, "Nombre").ToList();
            genero.Insert(0, new Genero { Id = null, Nombre = "Seleccione" });
            return new SelectList(genero, "Id", "Nombre", selectecItem);
        }

    }
}
