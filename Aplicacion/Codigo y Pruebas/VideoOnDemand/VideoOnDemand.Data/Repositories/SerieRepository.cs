using VideoOnDemand.Entities;
using VideoOnDemand.Data;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository (VideoOnDemandContext context) : base(context)
        {

        }
    }
}
