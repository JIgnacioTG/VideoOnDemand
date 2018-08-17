namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionGeneroOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Genero", "Descripcion", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genero", "Descripcion", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
