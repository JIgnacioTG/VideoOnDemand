using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class GeneroRepository : BaseRepository<Genero>
    {
        public GeneroRepository(VideoOnDemandContext context): base(context)
        {

        }
        public void LogicalDelete(Genero genero)
        {
            _context.Generos.Attach(genero);
            _context.Entry(genero).State = System.Data.Entity.EntityState.Modified;

            genero.Eliminado = true;
        }
    }
}
