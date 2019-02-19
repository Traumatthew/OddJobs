namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEstFrmJobMdl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "Estimate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Estimate", c => c.Double(nullable: false));
        }
    }
}
