using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;
using AppFramework.Expressions;


namespace VideoOnDemand.Web.Controllers
{
    public class ManageMovieController : BaseController
    {

        public SelectList GeneroList(object selectecItem = null)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(g => g.Eliminado == false, "Nombre").ToList();
            genero.Insert(0, new Genero { Id = null, Nombre = "Seleccione" });
            return new SelectList(genero, "Id", "Nombre", selectecItem);
        }

        // GET: Movie
        public ActionResult Index(int? idGenero,string nombre = "")
        {
            MovieRepository repository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var genero = generoRepository.Query(g => g.Eliminado == false, "Nombre");
            
            Expression<Func<Movie, bool>> expr = m => m.estado != EEstatusMedia.ELIMINADO;
            if (idGenero != null)
            {
                expr = expr.And(x => x.Generos.Any(y => y.Id == idGenero));
                
            }
            if(!string.IsNullOrEmpty(nombre))
            {
                expr = expr.And(x => x.nombre.Contains(nombre));              
            }
            var lst = repository.Query(expr, "Nombre");
            var model = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            var lstGenero = generoRepository.Query(g => g.Eliminado == false, "Nombre");


            ViewBag.ListaGenero = GeneroList(lstGenero);



            return View(model);


        }


       
        // GET: Movie/Create
        public ActionResult Create()
        {
            var model = new MovieViewModel();
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.Query(g => g.Eliminado == false, "Nombre");
            var lst2 = personaRepository.Query(p => p.Eliminado == false, "Nombre");
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);
            model.estado = EEstatusMedia.VISIBLE;
            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.Query(g => g.Eliminado == false, "Nombre");
            var lst2 = personaRepository.Query(p => p.Eliminado == false, "Nombre");
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            MovieRepository mov = new MovieRepository(context);
            

            try
            {
                MovieRepository repository = new MovieRepository(context);

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Movie movie = MapHelper.Map<Movie>(model);
                    var lstmov = mov.Query(m => m.estado != EEstatusMedia.ELIMINADO);
                    foreach (var m in lstmov)
                    {
                        if (m.nombre.ToLower() == movie.nombre.ToLower())
                        {
                            if(m.fechaLanzamiento == movie.fechaLanzamiento)
                            {
                                ViewBag.Error = 1;
                                return View(model);
                            }
                        }
                    }
                    repository.InsertComplete(movie, model.SeleccionarGeneros, model.SeleccionarPersonas);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            var repository = new MovieRepository(context);
            var generoRepository = new GeneroRepository(context);
            var personaRepository = new PersonaRepository(context);

            var includes = new Expression<Func<Movie, object>>[] { x => x.Generos };
            var includes2 = new Expression<Func<Movie, object>>[] { x => x.Actores };
            var movie = repository.QueryIncluding(x => x.id == id, includes).SingleOrDefault();
            var movie2 = repository.QueryIncluding(x => x.id == id, includes2).SingleOrDefault();

            var model = MapHelper.Map<MovieViewModel>(movie);

            var generos = generoRepository.Query(g => g.Eliminado == false, "Nombre");
            var personas = personaRepository.Query(p => p.Eliminado == false, "Nombre");

            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(personas);

            model.SeleccionarGeneros = movie.Generos.Select(x => x.Id.Value).ToArray();
            model.SeleccionarPersonas = movie.Actores.Select(x => x.Id.Value).ToArray();

            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieViewModel model)
        {
            var generoRepository = new GeneroRepository(context);
            var personaRepository = new PersonaRepository(context);
            MovieRepository mov = new MovieRepository(context);

            try
            {
                var repository = new MovieRepository(context);

                if (ModelState.IsValid)
                {
                    var movie = repository.Query(m => m.id == id).First();
                    movie = Update(movie, model);
                    var lstmov = mov.Query(m => m.estado != EEstatusMedia.ELIMINADO);
                    foreach (var m in lstmov)
                    {
                        if (m.id != movie.id)
                        {
                            if (m.nombre.ToLower() == movie.nombre.ToLower())
                            {
                                if (m.fechaLanzamiento == movie.fechaLanzamiento)
                                {
                                    ViewBag.Error = 1;
                                    return Edit(model.id);
                                }
                            }
                        }
                        
                    }
                    repository.UpdateComplete(movie, model.SeleccionarGeneros, model.SeleccionarPersonas);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                var generos = generoRepository.Query(g => g.Eliminado == false, "Nombre");
                var personas = personaRepository.Query(p => p.Eliminado == false, "Nombre");
                model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
                model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(personas);
                return Edit(model.id);
            }
            catch
            {
                return Edit(model.id);
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var movie = repository.Query(t => t.id == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);
            return View(model);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MovieViewModel   model)
        {
            try
            {
                MovieRepository repository = new MovieRepository(context);
                var movie = repository.Query(t => t.id == id).First();

                repository.DeleteIncomplete(movie);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Delete(id);
            }
        }

        public Movie Update(Movie movie, MovieViewModel model)
        {
            movie.descripcion = model.descripcion;
            movie.estado = model.estado;
            movie.fechaLanzamiento = model.fechaLanzamiento;
            movie.nombre = model.nombre;
            return movie;
        }

    }
}
