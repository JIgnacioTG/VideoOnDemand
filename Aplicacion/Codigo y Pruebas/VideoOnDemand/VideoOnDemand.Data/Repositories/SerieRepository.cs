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

        public void UpdateComplete(Serie serie, int[] generoIds, int[] personaIds)
        {
            _context.Medias.Attach(serie);
            _context.Entry(serie).State = System.Data.Entity.EntityState.Modified;

            //Recargamos la entidad
            _context.Entry(serie).Collection(x => x.Generos).Load();
            _context.Entry(serie).Collection(x => x.Actores).Load();

            //Limpiamos la lista
            serie.Generos.Clear();
            serie.Actores.Clear();

            if (generoIds != null && personaIds != null)
            {
                var generos = from g in _context.Generos
                              where generoIds.Contains((int)g.Id)
                              select g;

                var actores = from a in _context.Personas
                              where personaIds.Contains((int)a.Id)
                              select a;

                serie.Generos = new List<Genero>();
                serie.Actores = new List<Persona>();

                foreach (var g in generos)
                    serie.Generos.Add(g);

                foreach (var a in actores)
                    serie.Actores.Add(a);
            }
        }

        public void LogicalDelete(Serie serie)
        {
            _context.Medias.Attach(serie);
            _context.Entry(serie).State = System.Data.Entity.EntityState.Modified;

            serie.estado = EEstatusMedia.ELIMINADO;
        }

    }
}
