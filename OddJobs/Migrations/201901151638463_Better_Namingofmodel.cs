namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Better_Namingofmodel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContractorJobBids", newName: "JobBids");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.JobBids", newName: "ContractorJobBids");
        }
    }
}
