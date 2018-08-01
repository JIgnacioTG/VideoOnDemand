using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class MediaMap : EntityTypeConfiguration<Media>
    {

        public MediaMap()
        {

            ToTable("Media");

            HasKey(m => m.id);
            Property(m => m.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

    }
}
