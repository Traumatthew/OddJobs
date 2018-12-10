namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ContractorId = c.Int(nullable: false, identity: true),
                        ContractorName = c.String(),
                        ContractorPhone = c.String(),
                        AreaOfExpertise = c.String(),
                        ContractorEmail = c.String(),
                        ContractorStreet = c.String(),
                        ContractorState = c.String(),
                        ContractorCity = c.String(),
                        ContractorZip = c.String(),
                        TempRating = c.String(),
                        RatingData = c.String(),
                        Rating = c.Double(),
                        lat = c.String(),
                        lng = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContractorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRole = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        lat = c.String(),
                        lng = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CustomerWalletId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.CustomerWallets", t => t.CustomerWalletId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CustomerWalletId);
            
            CreateTable(
                "dbo.CustomerWallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        CardHolderName = c.String(),
                        CreditCardNumber = c.String(),
                        ExpirationDate = c.String(),
                        CVVNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GeoLocations",
                c => new
                    {
                        GeoID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Lat = c.String(),
                        Long = c.String(),
                    })
                .PrimaryKey(t => t.GeoID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Estimate = c.Double(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Details = c.String(),
                        CatId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ContractorId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Contractors", t => t.ContractorId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.JobCategories", t => t.CatId, cascadeDelete: true)
                .Index(t => t.CatId)
                .Index(t => t.CustomerId)
                .Index(t => t.ContractorId);
            
            CreateTable(
                "dbo.JobCategories",
                c => new
                    {
                        CatId = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                    })
                .PrimaryKey(t => t.CatId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Jobs", "CatId", "dbo.JobCategories");
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Jobs", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.GeoLocations", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CustomerWalletId", "dbo.CustomerWallets");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contractors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Jobs", new[] { "ContractorId" });
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            DropIndex("dbo.Jobs", new[] { "CatId" });
            DropIndex("dbo.GeoLocations", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "CustomerWalletId" });
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Contractors", new[] { "ApplicationUserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JobCategories");
            DropTable("dbo.Jobs");
            DropTable("dbo.GeoLocations");
            DropTable("dbo.CustomerWallets");
            DropTable("dbo.Customers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Contractors");
        }
    }
}
