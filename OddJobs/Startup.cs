using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OddJobs.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OddJobs.Startup))]
namespace OddJobs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Contractor"))
            {
                var contractorRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                contractorRole.Name = "Contractor";
                roleManager.Create(contractorRole);
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var customerRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                customerRole.Name = "Customer";
                roleManager.Create(customerRole);
            }
        }
    }
}
