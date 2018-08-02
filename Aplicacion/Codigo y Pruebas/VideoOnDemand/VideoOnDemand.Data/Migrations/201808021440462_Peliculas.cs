namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Peliculas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genero",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 100),
                        descripcion = c.String(maxLength: 500),
                        duracionMin = c.Int(nullable: false),
                        fechaRegistro = c.DateTime(nullable: false),
                        fechaLanzamiento = c.DateTime(),
                        estado = c.Int(),
                        Discriminator = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(maxLength: 500),
                        FechaNacimiento = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Puntuacion = c.Int(),
                        Descripcion = c.String(),
                        FechaRegistro = c.DateTime(),
                        UsuarioId = c.Int(),
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.ActoresMedias",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaId, t.ActorId })
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Persona", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.GenerosMedias",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                        GeneroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaId, t.GeneroId })
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Genero", t => t.GeneroId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.GeneroId);
            
            CreateTable(
                "dbo.Episodio",
                c => new
                    {
                        id = c.Int(nullable: false),
                        Serie_id = c.Int(),
                        temporada = c.Int(nullable: false),
                        serieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.id)
                .ForeignKey("dbo.Media", t => t.Serie_id)
                .ForeignKey("dbo.Media", t => t.serieId)
                .Index(t => t.id)
                .Index(t => t.Serie_id)
                .Index(t => t.serieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episodio", "serieId", "dbo.Media");
            DropForeignKey("dbo.Episodio", "Serie_id", "dbo.Media");
            DropForeignKey("dbo.Episodio", "id", "dbo.Media");
            DropForeignKey("dbo.Opinions", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Opinions", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.GenerosMedias", "GeneroId", "dbo.Genero");
            DropForeignKey("dbo.GenerosMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.ActoresMedias", "ActorId", "dbo.Persona");
            DropForeignKey("dbo.ActoresMedias", "MediaId", "dbo.Media");
            DropIndex("dbo.Episodio", new[] { "serieId" });
            DropIndex("dbo.Episodio", new[] { "Serie_id" });
            DropIndex("dbo.Episodio", new[] { "id" });
            DropIndex("dbo.GenerosMedias", new[] { "GeneroId" });
            DropIndex("dbo.GenerosMedias", new[] { "MediaId" });
            DropIndex("dbo.ActoresMedias", new[] { "ActorId" });
            DropIndex("dbo.ActoresMedias", new[] { "MediaId" });
            DropIndex("dbo.Opinions", new[] { "MediaId" });
            DropIndex("dbo.Opinions", new[] { "UsuarioId" });
            DropTable("dbo.Episodio");
            DropTable("dbo.GenerosMedias");
            DropTable("dbo.ActoresMedias");
            DropTable("dbo.Opinions");
            DropTable("dbo.Persona");
            DropTable("dbo.Media");
            DropTable("dbo.Genero");
        }
    }
}
