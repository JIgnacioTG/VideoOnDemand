namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorito",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FechaAgregado = c.DateTime(nullable: false),
                        usuarioId = c.Int(nullable: false),
                        mediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.mediaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.usuarioId, cascadeDelete: true)
                .Index(t => t.usuarioId)
                .Index(t => t.mediaId);
            
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
                "dbo.Genero",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Opinion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Puntuacion = c.Int(),
                        Descripcion = c.String(maxLength: 500),
                        FechaRegistro = c.DateTime(),
                        UsuarioId = c.Int(),
                        MediaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .Index(t => t.UsuarioId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        IdentityId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MediaOnPlay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Milisegundo = c.Long(),
                        MediaId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.UsuarioId);
            
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
                        temporada = c.Int(nullable: false),
                        serieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.id)
                .ForeignKey("dbo.Serie", t => t.serieId)
                .Index(t => t.id)
                .Index(t => t.serieId);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Serie",
                c => new
                    {
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.id)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Serie", "id", "dbo.Media");
            DropForeignKey("dbo.Movie", "id", "dbo.Media");
            DropForeignKey("dbo.Episodio", "serieId", "dbo.Serie");
            DropForeignKey("dbo.Episodio", "id", "dbo.Media");
            DropForeignKey("dbo.MediaOnPlay", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MediaOnPlay", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Favorito", "usuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Favorito", "mediaId", "dbo.Media");
            DropForeignKey("dbo.Opinion", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Opinion", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.GenerosMedias", "GeneroId", "dbo.Genero");
            DropForeignKey("dbo.GenerosMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.ActoresMedias", "ActorId", "dbo.Persona");
            DropForeignKey("dbo.ActoresMedias", "MediaId", "dbo.Media");
            DropIndex("dbo.Serie", new[] { "id" });
            DropIndex("dbo.Movie", new[] { "id" });
            DropIndex("dbo.Episodio", new[] { "serieId" });
            DropIndex("dbo.Episodio", new[] { "id" });
            DropIndex("dbo.GenerosMedias", new[] { "GeneroId" });
            DropIndex("dbo.GenerosMedias", new[] { "MediaId" });
            DropIndex("dbo.ActoresMedias", new[] { "ActorId" });
            DropIndex("dbo.ActoresMedias", new[] { "MediaId" });
            DropIndex("dbo.MediaOnPlay", new[] { "UsuarioId" });
            DropIndex("dbo.MediaOnPlay", new[] { "MediaId" });
            DropIndex("dbo.Opinion", new[] { "MediaId" });
            DropIndex("dbo.Opinion", new[] { "UsuarioId" });
            DropIndex("dbo.Favorito", new[] { "mediaId" });
            DropIndex("dbo.Favorito", new[] { "usuarioId" });
            DropTable("dbo.Serie");
            DropTable("dbo.Movie");
            DropTable("dbo.Episodio");
            DropTable("dbo.GenerosMedias");
            DropTable("dbo.ActoresMedias");
            DropTable("dbo.MediaOnPlay");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Opinion");
            DropTable("dbo.Genero");
            DropTable("dbo.Persona");
            DropTable("dbo.Media");
            DropTable("dbo.Favorito");
        }
    }
}
