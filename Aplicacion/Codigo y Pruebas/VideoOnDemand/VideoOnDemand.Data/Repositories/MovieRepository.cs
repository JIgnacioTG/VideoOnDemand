using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MovieRepository : BaseRepository<Movie>
    {

        public MovieRepository(VideoOnDemandContext context) : base (context)
        {

        }

    }
}
