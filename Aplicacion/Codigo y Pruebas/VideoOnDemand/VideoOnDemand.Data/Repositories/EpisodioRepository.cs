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

        public void LogicalDelete(Episodio episodio)
        {
            _context.Medias.Attach(episodio);
            _context.Entry(episodio).State = System.Data.Entity.EntityState.Modified;

            episodio.estado = EEstatusMedia.ELIMINADO;
        }

    }
}
