namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eliminacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persona", "Eliminado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Genero", "Eliminado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Genero", "Eliminado");
            DropColumn("dbo.Persona", "Eliminado");
        }
    }
}
