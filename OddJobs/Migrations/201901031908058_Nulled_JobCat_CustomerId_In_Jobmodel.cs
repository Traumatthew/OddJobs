namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nulled_JobCat_CustomerId_In_Jobmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Jobs", "CatId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CatId" });
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            AlterColumn("dbo.Jobs", "CatId", c => c.Int());
            AlterColumn("dbo.Jobs", "CustomerId", c => c.Int());
            CreateIndex("dbo.Jobs", "CatId");
            CreateIndex("dbo.Jobs", "CustomerId");
            AddForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Jobs", "CatId", "dbo.JobCategories", "CatId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CatId", "dbo.JobCategories");
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            DropIndex("dbo.Jobs", new[] { "CatId" });
            AlterColumn("dbo.Jobs", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "CatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CustomerId");
            CreateIndex("dbo.Jobs", "CatId");
            AddForeignKey("dbo.Jobs", "CatId", "dbo.JobCategories", "CatId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
