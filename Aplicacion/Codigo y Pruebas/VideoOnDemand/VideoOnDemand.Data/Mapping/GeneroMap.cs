using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class GeneroMap : EntityTypeConfiguration<Genero>
    {

        public GeneroMap()
        {

            ToTable("Genero");

            HasKey(g => g.Id);
            Property(g => g.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.Nombre).HasMaxLength(50).IsRequired();
            Property(g => g.Descripcion).HasMaxLength(500).IsRequired();
            Property(g => g.Eliminado).IsRequired();

        }

    }
}
