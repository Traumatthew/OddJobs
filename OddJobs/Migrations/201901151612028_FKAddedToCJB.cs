namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKAddedToCJB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorJobBids", "ContractorId", c => c.Int());
            CreateIndex("dbo.ContractorJobBids", "ContractorId");
            AddForeignKey("dbo.ContractorJobBids", "ContractorId", "dbo.Contractors", "ContractorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractorJobBids", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.ContractorJobBids", new[] { "ContractorId" });
            DropColumn("dbo.ContractorJobBids", "ContractorId");
        }
    }
}
