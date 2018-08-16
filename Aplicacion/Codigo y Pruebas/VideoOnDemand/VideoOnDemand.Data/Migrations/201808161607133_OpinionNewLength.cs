namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpinionNewLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Opinion", "Descripcion", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Opinion", "Descripcion", c => c.String(maxLength: 500));
        }
    }
}
