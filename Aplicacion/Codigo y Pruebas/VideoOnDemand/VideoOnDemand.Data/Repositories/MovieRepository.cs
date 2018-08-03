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

    }
}
