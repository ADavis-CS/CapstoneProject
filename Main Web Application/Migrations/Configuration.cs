namespace Main_Web_Application.Migrations
{
    using Main_Web_Application.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;



    internal sealed class MembershipConfiguration : DbMigrationsConfiguration<Main_Web_Application.Models.ApplicationDbContext>
    {
        public MembershipConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }



        protected override void Seed(Main_Web_Application.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.Email == "alan.davis@snhu.edu"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "alan.davis@snhu.edu", Email = "alan.davis@snhu.edu" };

                manager.Create(user, "xPI3WvE8yd6B");
                manager.AddToRole(user.Id, "Admin");
            }
        }


    }



    internal sealed class Configuration : DbMigrationsConfiguration<Main_Web_Application.Models.CollectablesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Main_Web_Application.Models.CollectablesDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
