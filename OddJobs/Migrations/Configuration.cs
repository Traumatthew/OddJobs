namespace OddJobs.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OddJobs.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(OddJobs.Models.ApplicationDbContext context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data.
        //    context.JobCategories.AddOrUpdate(j => j.CatId,

        //        new Models.JobCategory() { CatId = 1, CatName = "Landscaping" },
        //        new Models.JobCategory() { CatId = 2, CatName = "Plumbing" },
        //        new Models.JobCategory() { CatId = 3, CatName = "Electrical" },
        //        new Models.JobCategory() { CatId = 4, CatName = "Roofing" },
        //        new Models.JobCategory() { CatId = 5, CatName = "Remodeling" },
        //        new Models.JobCategory() { CatId = 6, CatName = "Cleaning" },
        //        new Models.JobCategory() { CatId = 7, CatName = "GeneralLabor" },
        //        new Models.JobCategory() { CatId = 8, CatName = "BabySitting" }

        //);
        //}
    }
}
