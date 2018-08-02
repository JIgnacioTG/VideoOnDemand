using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class SerieMap : EntityTypeConfiguration<Serie>
    {
        public SerieMap()
        {
            ToTable("Serie");

            HasKey(s => s.id);
            Property(s => s.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
