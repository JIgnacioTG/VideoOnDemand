using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class PersonaRepository : BaseRepository<Persona>
    {

        public PersonaRepository(VideoOnDemandContext context) : base(context)
        {

        }
        public void LogicalDelete(Persona persona)
        {
            _context.Personas.Attach(persona);
            _context.Entry(persona).State = System.Data.Entity.EntityState.Modified;

            persona.Eliminado = true;
        }
    }
}
