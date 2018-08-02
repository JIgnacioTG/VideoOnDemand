using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class OpinionMap : EntityTypeConfiguration<Opinion>
    {

        public OpinionMap()
        {

            ToTable("Opinion");

            HasKey(o => o.Id);
            Property(o => o.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.Puntuacion)
                .IsOptional();
            Property(o => o.Descripcion)
                .HasMaxLength(500)
                .IsOptional();
            Property(o => o.FechaRegistro)
                .IsOptional();

        }

    }
}
