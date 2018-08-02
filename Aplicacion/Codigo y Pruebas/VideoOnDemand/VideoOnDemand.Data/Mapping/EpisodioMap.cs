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

            HasKey(e => e.id);
            Property(e => e.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.temporada)
                .IsRequired();
            Property(e => e.serieId)
                .IsRequired();

            HasRequired(e => e.Serie)
                .WithMany()
                .HasForeignKey(e => e.serieId);

        }

    }
}
