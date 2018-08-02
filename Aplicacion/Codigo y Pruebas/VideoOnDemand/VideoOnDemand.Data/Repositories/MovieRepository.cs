using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MovieRepository : BaseRepository<Movie>
    {

        public MovieRepository(VideoOnDemandContext context) : base (context)
        {

        }

        public void InsertComplete (Movie movie, int[] generoId, int[] personaId)
        {
            if(generoId != null && personaId != null)
            {
                var genero = from t in _context.Generos
                             where generoId.Con
            }
        }

    }
}
