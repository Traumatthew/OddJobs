namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLatLngToJobModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Street", c => c.String());
            AddColumn("dbo.Jobs", "City", c => c.String());
            AddColumn("dbo.Jobs", "State", c => c.String());
            AddColumn("dbo.Jobs", "Zip", c => c.String());
            AddColumn("dbo.Jobs", "lat", c => c.String());
            AddColumn("dbo.Jobs", "lng", c => c.String());
            DropColumn("dbo.Jobs", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Location", c => c.String(nullable: false));
            DropColumn("dbo.Jobs", "lng");
            DropColumn("dbo.Jobs", "lat");
            DropColumn("dbo.Jobs", "Zip");
            DropColumn("dbo.Jobs", "State");
            DropColumn("dbo.Jobs", "City");
            DropColumn("dbo.Jobs", "Street");
        }
    }
}
