using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository (VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Movie movie, int[] generoIds, int[] personaIds)
        {
            if (generoIds != null && personaIds != null)
            {
                var genero = from g in _context.Generos
                             where generoIds.Contains(g.Id.Value)
                             select g;

                var persona = from p in _context.Personas
                              where personaIds.Contains(p.Id.Value)
                              select p;

                movie.Generos = new List<Genero>();
                foreach (var g in genero)
                {
                    movie.Generos.Add(g);
                }

                movie.Actores = new List<Persona>();
                foreach (var p in persona)
                {
                    movie.Actores.Add(p);
                }

                _context.Medias.Add(movie);

            }
        }

    }
}
