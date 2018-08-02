using ltracker.Data.Repositories;
using VideoOnDemand.Entities;
using VideoOnDemand.Data;

namespace VideoOnDemand.Repositories
{
    public class EpisodioRepository : BaseRepository<Episodio>
    {
        public EpisodioRepository(VideoOnDemandContext context) : base(context)
        {

        }

    }
}
