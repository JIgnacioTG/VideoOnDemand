using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VideoOnDemand.Repositories
{
    public class EpisodioRepository : BaseRepository<Episodio>
    {
        public EpisodioRepository(VideoOnDemandContext context) : base(context)
        {

        }

        public void InsertComplete(Episodio episodio, int[] generoIds, int[] personaIds)
        {
            if (generoIds != null && personaIds != null)
            {
                var genero = from g in _context.Generos
                             where generoIds.Contains(g.Id.Value)
                             select g;

                var persona = from p in _context.Personas
                              where personaIds.Contains(p.Id.Value)
                              select p;

                episodio.Generos = new List<Genero>();
                foreach (var g in genero)
                {
                    episodio.Generos.Add(g);
                }

                episodio.Actores = new List<Persona>();
                foreach (var p in persona)
                {
                    episodio.Actores.Add(p);
                }

                episodio.fechaRegistro = DateTime.Now;
                episodio.estado = EEstatusMedia.VISIBLE;
            }
        }

        public void UpdateComplete(Episodio episodio, int[] generoIds, int[] personaIds)
        {
            _context.Medias.Attach(episodio);
            _context.Entry(episodio).State = System.Data.Entity.EntityState.Modified;

            //Recargamos la entidad
            _context.Entry(episodio).Collection(x => x.Generos).Load();
            _context.Entry(episodio).Collection(x => x.Actores).Load();

            //Limpiamos la lista
            episodio.Generos.Clear();
            episodio.Actores.Clear();

            if (generoIds != null && personaIds != null)
            {
                var generos = from g in _context.Generos
                              where generoIds.Contains((int)g.Id)
                              select g;

                var actores = from a in _context.Personas
                              where personaIds.Contains((int)a.Id)
                              select a;

                episodio.Generos = new List<Genero>();
                episodio.Actores = new List<Persona>();

                foreach (var g in generos)
                    episodio.Generos.Add(g);

                foreach (var a in actores)
                    episodio.Actores.Add(a);
            }
        }

        public void LogicalDelete(Episodio episodio)
        {
            _context.Medias.Attach(episodio);
            _context.Entry(episodio).State = System.Data.Entity.EntityState.Modified;

            episodio.estado = EEstatusMedia.ELIMINADO;
        }

    }
}
