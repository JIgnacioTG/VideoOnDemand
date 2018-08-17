using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class PersonaController : BaseController
    {

        public struct lstMedias
        {
            public int? id;
            public string nombre;
            public string tipo;
        }
        // GET: Persona
        public ActionResult Index()
        {
            PersonaRepository repository = new PersonaRepository(context);
            //Consulte los Individuas del repositorio
            var lst = repository.Query(g => g.Eliminado != true, "Nombre");
            //Mapeamos la lista de Individuos
            var models = MapHelper.Map<IEnumerable<PersonaViewModel>>(lst);

            return View(models);
        }

        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        public ActionResult Create(PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonaRepository repository = new PersonaRepository(context);

                    #region Validaciones
                    //Nombre Unico
                    var actorName = new Persona { Nombre = model.Nombre };

                    bool existPersona = repository.QueryByExample(actorName).Count > 0;

                    if (existPersona)
                    {
                        ModelState.AddModelError("Nombre", "El Nombre del Actor ya Existe");
                        return View(model);
                    }

                    #endregion

                    Persona persona = MapHelper.Map<Persona>(model);
                    persona.Eliminado = false;
                    repository.Insert(persona);

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

        // GET: Persona/Edit/5
        public ActionResult Edit(int id)
        {

            PersonaRepository repository = new PersonaRepository(context);
            var actor = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<PersonaViewModel>(actor);

            return View(model);

        }

        // POST: Persona/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonaRepository repository = new PersonaRepository(context);
                    
                    bool existeTopic = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;

                    if (existeTopic)
                    {
                        ModelState.AddModelError("Name", "El Nombre del ACtor ya Existe");
                        return View(model);
                    }

                    Persona topic = MapHelper.Map<Persona>(model);
                    repository.Update(topic);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int? id)
        {
            PersonaRepository repository = new PersonaRepository(context);
            var actor = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<PersonaViewModel>(actor);
            if (ViewBag.Error == 1)
            {
                ViewBag.Salto = 1;
            }
            else
            {
                ViewBag.Salto = 0;
            }
            return View(model);
        }

        // POST: Persona/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PersonaViewModel model)
        {
            try
            {


                MediaRepository media = new MediaRepository(context);
                PersonaRepository repo = new PersonaRepository(context);

                var pel = media.GetAll();
                var actor = repo.Query(g => g.Id == id).FirstOrDefault();

                context.Entry(actor).Collection(m => m.Medias).Load();

                if(actor.Medias.Count() == 0)
                {
                    repo.LogicalDelete(actor);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    List<lstMedias> lista = new List<lstMedias>();
                    var repository = new MovieRepository(context);
                    var repository2 = new SerieRepository(context);

                    foreach (var medi in actor.Medias)
                    {
                        var mov = repository.Query(m => m.id == medi.id && medi.estado != EEstatusMedia.ELIMINADO);
                        var ser = repository2.Query(s => s.id == medi.id && medi.estado != EEstatusMedia.ELIMINADO);

                        if (mov.Count() == 1)
                        {
                            lista.Add(new lstMedias() { id = medi.id, nombre = medi.nombre, tipo = "Película" });
                        }
                        else if (ser.Count() == 1)
                        {
                            lista.Add(new lstMedias() { id = medi.id, nombre = medi.nombre, tipo = "Serie" });
                        }

                    }

                    if (lista.Count == 0)
                    {
                        repo.LogicalDelete(actor);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewData["inUsing"] = lista;
                    ViewBag.Error = 1;
                    model = MapHelper.Map<PersonaViewModel>(actor);
                    return Delete(model.Id);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
