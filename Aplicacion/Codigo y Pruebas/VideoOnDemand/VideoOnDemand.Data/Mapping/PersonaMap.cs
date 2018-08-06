using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Mapping
{
    class PersonaMap : EntityTypeConfiguration<Persona>
    {
        public PersonaMap()
        {
            ToTable("Persona");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            Property(p => p.Descripcion).HasMaxLength(500).IsOptional();
            Property(p => p.FechaNacimiento).IsOptional();
            Property(p => p.Eliminado).IsRequired();

        }
    }
}
