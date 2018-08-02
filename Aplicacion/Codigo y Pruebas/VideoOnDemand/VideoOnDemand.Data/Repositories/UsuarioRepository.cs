using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>
    {
        public UsuarioRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
