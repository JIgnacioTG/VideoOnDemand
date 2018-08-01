using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class MediaOnPlayMap : EntityTypeConfiguration<MediaOnPlay>
    {

        public MediaOnPlayMap()
        {

            ToTable("MediaOnPlay");
            HasKey(m => m.Id);
            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.Milisegundo)
                .IsOptional();

            HasRequired(m => m.Media)
                .WithMany()
                .HasForeignKey(m => m.MediaId);

            HasRequired(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.UsuarioId);

        }

    }
}
