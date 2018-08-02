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

            HasMany(s => s.Episodios)
                .WithRequired(e => e.Serie)
                .HasForeignKey(e => e.serieId);

        }
    }
}
