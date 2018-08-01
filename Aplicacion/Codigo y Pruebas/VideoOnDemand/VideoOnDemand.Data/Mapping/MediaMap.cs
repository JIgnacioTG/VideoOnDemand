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
            Property(m => m.nombre)
                .HasMaxLength(100)
                .IsRequired();
            Property(m => m.descripcion)
                .HasMaxLength(500)
                .IsOptional();
            Property(m => m.duracionMin)
                .IsRequired();
            Property(m => m.fechaLanzamiento)
                .IsOptional();
            Property(m => m.fechaRegistro)
                .IsRequired();

            HasMany(m => m.Generos)
                .WithMany(g => g.Medias)
                .Map(m => {
                    m.MapRightKey("GeneroId");
                    m.MapLeftKey("MediaId");
                    m.ToTable("GenerosMedias");
                });

            HasMany(m => m.Actores)
                .WithMany(p => p.Medias)
                .Map(m => {
                    m.MapRightKey("ActorId");
                    m.MapLeftKey("MediaId");
                    m.ToTable("ActoresMedias");
                });

            HasMany(m => m.Opiniones)
                .WithRequired(o => o.Media)
                .HasForeignKey(o => o.MediaId);

        }

    }
}
