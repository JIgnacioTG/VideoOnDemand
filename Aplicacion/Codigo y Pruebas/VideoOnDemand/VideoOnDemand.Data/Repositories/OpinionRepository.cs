using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class OpinionRepository : BaseRepository<Opinion>
    {

        public OpinionRepository(VideoOnDemandContext context) : base(context)
        {

        }

    }
}
