using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class MovieMap : EntityTypeConfiguration<Movie>
    {
        public MovieMap()
        {
            ToTable("Movie");

            HasKey(m => m.id);
            Property(m => m.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


        }

    }
}
