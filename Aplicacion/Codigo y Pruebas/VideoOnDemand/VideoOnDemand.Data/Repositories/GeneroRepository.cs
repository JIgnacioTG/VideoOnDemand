using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class GeneroRepository : BaseRepository<Genero>
    {
        public GeneroRepository(VideoOnDemandContext context): base(context)
        {

        }
    }
}
