using VideoOnDemand.Entities;
using VideoOnDemand.Data;

namespace VideoOnDemand.Repositories
{
    public class FavoritoRepository : BaseRepository<Favorito>
    {
        public FavoritoRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
