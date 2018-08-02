using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class EpisodioMap : EntityTypeConfiguration<Episodio>
    {

        public EpisodioMap()
        {

            ToTable("Episodio");

            Property(e => e.temporada)
                .IsRequired();
            Property(e => e.serieId)
                .IsRequired();

        }

    }
}
