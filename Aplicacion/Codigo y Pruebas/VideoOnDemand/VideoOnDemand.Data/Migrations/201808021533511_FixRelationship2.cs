namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationship2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Episodio", new[] { "serieId" });
            AlterColumn("dbo.Episodio", "serieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodio", "serieId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Episodio", new[] { "serieId" });
            AlterColumn("dbo.Episodio", "serieId", c => c.Int());
            CreateIndex("dbo.Episodio", "serieId");
        }
    }
}
