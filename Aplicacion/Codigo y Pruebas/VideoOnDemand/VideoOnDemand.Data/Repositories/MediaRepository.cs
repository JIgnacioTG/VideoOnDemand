using ltracker.Data.Repositories;
using VideoOnDemand.Entities;
using VideoOnDemand.Data;

namespace VideoOnDemand.Repositories
{
    public class MediaRepository : BaseRepository<Media>
    {
        public MediaRepository (VideoOnDemandContext context) : base(context)
        {

        }
    }
}
