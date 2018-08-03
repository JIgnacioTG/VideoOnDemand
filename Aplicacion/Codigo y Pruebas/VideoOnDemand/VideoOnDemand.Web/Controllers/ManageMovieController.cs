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

namespace VideoOnDemand.Web.Controllers
{
    public class ManageMovieController : BaseController
    {
        // GET: Movie
        public ActionResult Index()
        {
            MovieRepository repository = new MovieRepository(context);

            var lst = repository.GetAll();

            var model = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);


            return View(model);
        }
       
        // GET: Movie/Create
        public ActionResult Create()
        {
            var model = new MovieViewModel();
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.GetAll();
            var lst2 = personaRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);
            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst = generoRepository.GetAll();
            var lst2 = personaRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            try
            {
                MovieRepository repository = new MovieRepository(context);

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Movie movie = MapHelper.Map<Movie>(model);
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
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new MovieRepository(context);
            var generoRepository = new GeneroRepository(context);
            var personaRepository = new PersonaRepository(context);

            var includes = new Expression<Func<Movie, object>>[] { x => x.Generos };
            var includes2 = new Expression<Func<Movie, object>>[] { x => x.Actores };
            var movie = repository.QueryIncluding(x => x.id == id, includes).SingleOrDefault();
            var movie2 = repository.QueryIncluding(x => x.id == id, includes2).SingleOrDefault();

            var model = MapHelper.Map<MovieViewModel>(movie);

            var generos = generoRepository.Query(null, "Nombre");
            var personas = personaRepository.Query(null, "Nombre");

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

            try
            {
                var repository = new MovieRepository(context);

                if (ModelState.IsValid)
                {
                    var movie = MapHelper.Map<Movie>(model);
                    repository.UpdateComplete(movie, model.SeleccionarGeneros, model.SeleccionarPersonas);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                var genero = generoRepository.Query(null, "Nombre");
                var actores = personaRepository.Query(null, "Nombre");
                model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(genero);
                model.PersonasDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
                return View(model);
            }
            catch(Exception ex)
            {
                return View(model);
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
                return RedirectToAction("Index");
            }
        }
    }
}
