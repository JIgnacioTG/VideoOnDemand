namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnumEpisodio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodio", "numEpisodio", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Episodio", "numEpisodio");
        }
    }
}
