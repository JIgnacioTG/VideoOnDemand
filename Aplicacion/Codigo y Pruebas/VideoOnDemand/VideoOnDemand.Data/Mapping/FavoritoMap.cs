using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class FavoritoMap : EntityTypeConfiguration<Favorito>
    {
        public FavoritoMap()
        {
            ToTable("Favorito");
            HasKey(m => m.id);
            Property(m => m.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.FechaAgregado)
                .IsRequired();
            Property(m => m.mediaId)
                .IsRequired();
            HasRequired(m => m.media)
                .WithMany()
                .HasForeignKey(m => m.mediaId);
            Property(m => m.usuarioId)
                .IsRequired();
            HasRequired(m => m.usuario)
                .WithMany()
                .HasForeignKey(m => m.usuarioId);

        }
    }
}
