using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository (VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Serie serie, int[] generoIds, int[] personaIds)
        {
            if (generoIds != null && personaIds != null)
            {
                var genero = from g in _context.Generos
                             where generoIds.Contains(g.Id.Value)
                             select g;

                var persona = from p in _context.Personas
                              where personaIds.Contains(p.Id.Value)
                              select p;

                serie.Generos = new List<Genero>();
                foreach (var g in genero)
                {
                    serie.Generos.Add(g);
                }

                serie.Actores = new List<Persona>();
                foreach (var p in persona)
                {
                    serie.Actores.Add(p);
                }

                serie.fechaRegistro = DateTime.Now;
                serie.estado = EEstatusMedia.VISIBLE;

                _context.Medias.Add(serie);

            }
        }

    }
}
