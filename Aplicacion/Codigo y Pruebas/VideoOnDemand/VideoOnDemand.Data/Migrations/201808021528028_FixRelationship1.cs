namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationship1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Opinion", "MediaId", "dbo.Media");
            DropIndex("dbo.Opinion", new[] { "MediaId" });
            DropIndex("dbo.Episodio", new[] { "serieId" });
            AlterColumn("dbo.Episodio", "serieId", c => c.Int());
            AlterColumn("dbo.Opinion", "MediaId", c => c.Int());
            CreateIndex("dbo.Opinion", "MediaId");
            CreateIndex("dbo.Episodio", "serieId");
            AddForeignKey("dbo.Opinion", "MediaId", "dbo.Media", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinion", "MediaId", "dbo.Media");
            DropIndex("dbo.Episodio", new[] { "serieId" });
            DropIndex("dbo.Opinion", new[] { "MediaId" });
            AlterColumn("dbo.Opinion", "MediaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Episodio", "serieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodio", "serieId");
            CreateIndex("dbo.Opinion", "MediaId");
            AddForeignKey("dbo.Opinion", "MediaId", "dbo.Media", "id", cascadeDelete: true);
        }
    }
}
