namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedNames : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobBids", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.JobBids", "JobId", "dbo.Jobs");
            DropIndex("dbo.JobBids", new[] { "JobId" });
            DropIndex("dbo.JobBids", new[] { "ContractorId" });
            CreateTable(
                "dbo.Estimates",
                c => new
                    {
                        EstId = c.Int(nullable: false, identity: true),
                        EstHoursToComplete = c.Int(),
                        MaterialCost = c.Int(),
                        Date = c.DateTime(),
                        BidAmt = c.Int(),
                        JobId = c.Int(nullable: false),
                        ContractorId = c.Int(),
                    })
                .PrimaryKey(t => t.EstId)
                .ForeignKey("dbo.Contractors", t => t.ContractorId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.ContractorId);
            
            DropTable("dbo.JobBids");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobBids",
                c => new
                    {
                        BidId = c.Int(nullable: false, identity: true),
                        EstHoursToComplete = c.Int(),
                        MaterialCost = c.Int(),
                        Date = c.DateTime(),
                        BidAmt = c.Int(),
                        JobId = c.Int(nullable: false),
                        ContractorId = c.Int(),
                    })
                .PrimaryKey(t => t.BidId);
            
            DropForeignKey("dbo.Estimates", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Estimates", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Estimates", new[] { "ContractorId" });
            DropIndex("dbo.Estimates", new[] { "JobId" });
            DropTable("dbo.Estimates");
            CreateIndex("dbo.JobBids", "ContractorId");
            CreateIndex("dbo.JobBids", "JobId");
            AddForeignKey("dbo.JobBids", "JobId", "dbo.Jobs", "JobId", cascadeDelete: true);
            AddForeignKey("dbo.JobBids", "ContractorId", "dbo.Contractors", "ContractorId");
        }
    }
}
