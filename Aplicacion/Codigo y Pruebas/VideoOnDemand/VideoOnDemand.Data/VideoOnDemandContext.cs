using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Data
{
    public class VideoOnDemandContext : DbContext
    {
        public VideoOnDemandContext() : base("name=VideoOnDemandContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Serie> Series { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var usuarioEntity = modelBuilder.Entity<Usuario>();
            usuarioEntity.HasKey(x => x.Id);
            usuarioEntity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            usuarioEntity.Property(x => x.Nombre).HasMaxLength(200).IsRequired();
            usuarioEntity.Property(x => x.IdentityId).HasMaxLength(128).IsRequired();

            var episodioEntity = modelBuilder.Entity<Episodio>();
            episodioEntity.HasKey(e => e.id);
            episodioEntity.Property(e => e.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            episodioEntity.Property(e => e.temporada)
                .IsRequired();
            episodioEntity.Property(e => e.serieId)
                .IsOptional();
            
        }
    }
}
