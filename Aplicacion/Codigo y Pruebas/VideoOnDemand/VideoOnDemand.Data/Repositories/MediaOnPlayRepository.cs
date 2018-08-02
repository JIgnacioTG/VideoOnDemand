using VideoOnDemand.Entities;
using VideoOnDemand.Data;

namespace VideoOnDemand.Repositories
{
    public class MediaOnPlayRepository : BaseRepository<MediaOnPlay>
    {
        public MediaOnPlayRepository (VideoOnDemandContext context) : base(context)
        {

        }
    }
}
