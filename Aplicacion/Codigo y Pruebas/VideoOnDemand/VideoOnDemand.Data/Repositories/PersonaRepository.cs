using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class PersonaRepository : BaseRepository<Persona>
    {

        public PersonaRepository(VideoOnDemandContext context) : base(context)
        {

        }

    }
}
