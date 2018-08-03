using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VideoOnDemand.Repositories
{
    public class MovieRepository : BaseRepository<Movie>
    {

        public MovieRepository(VideoOnDemandContext context) : base (context)
        {

        }

        public void InsertComplete (Movie movie, int[] generoId, int[] personaId)
        {
            if(generoId != null && personaId != null)
            {
                var genero = from t in _context.Generos
                             where generoId.Contains(t.Id.Value)
                             select t;

                var persona = from p in _context.Personas
                              where personaId.Contains(p.Id.Value)
                              select p;

                movie.Generos = new List<Genero>();
                foreach(var t in genero)
                {
                    movie.Generos.Add(t);
                }

                movie.Actores = new List<Persona>();
                foreach(var p in persona)
                {
                    movie.Actores.Add(p);
                }
                movie.fechaRegistro = DateTime.Now;
                movie.estado = EEstatusMedia.VISIBLE;

                _context.Medias.Add(movie);
                
            }
            
        }

        public void UpdateComplete (Movie movie, int[] seleccionarGenero, int[] seleccionarActor)
        {
            _context.Medias.Attach(movie);
            _context.Entry(movie).State = System.Data.Entity.EntityState.Modified;

            //Recargamos la entidad
            _context.Entry(movie).Collection(x => x.Generos).Load();
            _context.Entry(movie).Collection(x => x.Actores).Load();

            //Limpiamos la lista
            movie.Generos.Clear();
            movie.Actores.Clear();

            if(seleccionarGenero != null && seleccionarActor != null)
            {
                var generos = from g in _context.Generos
                            where seleccionarGenero.Contains((int)g.Id)
                            select g;

                var actores = from a in _context.Personas
                              where seleccionarActor.Contains((int)a.Id)
                              select a;

                movie.Generos = new List<Genero>();
                movie.Actores = new List<Persona>();

                

                foreach (var g in generos)
                    movie.Generos.Add(g);

                foreach (var a in actores)
                    movie.Actores.Add(a);
            }
        }

        public void DeleteIncomplete (Movie movie)
        {
            _context.Medias.Attach(movie);
            _context.Entry(movie).State = System.Data.Entity.EntityState.Modified;
            

            movie.estado = EEstatusMedia.ELIMINADO;
        }
    }
}
