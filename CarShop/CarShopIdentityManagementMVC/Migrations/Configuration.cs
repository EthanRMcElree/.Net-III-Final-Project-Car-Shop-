namespace CarShopIdentityManagementMVC.Migrations
{
    using CarShopIdentityManagementMVC.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LogicLayer;

    internal sealed class Configuration : DbMigrationsConfiguration<CarShopIdentityManagementMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarShopIdentityManagementMVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            var roles = new string[] { "ADMIN", "CUSTOMER", "EMPLOYEE", "MANAGER", "USER" };
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("ADMIN"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "ADMIN" });
            }

            MakeUser("jake@company.com", "Jake", "Wellington", "Test-123", "MANAGER", context);
            MakeUser("john@company.com", "John", "Wellington", "Test-123", "ADMIN", context);
            MakeUser("sam@gmail.com", "Sam", "Searing", "Test-123", "CUSTOMER", context);
            MakeUser("max@gmail.com", "Max", "Harding", "Test-123", "CUSTOMER", context);
            MakeUser("luke@gmail.com", "Luke", "Maring", "Test-123", "CUSTOMER", context);
            MakeUser("martin@gmail.com", "Martin", "Dwyer", "Test-123", "USER", context);
            MakeUser("ethan@company.com", "Ethan", "McElree", "Test-123", "EMPLOYEE", context);
        }

        private void MakeUser(string userEmail, string userFirstName, string userLastName, string userPassword, string userRole, CarShopIdentityManagementMVC.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string user = userEmail;
            string password = userPassword;
            if (!context.Users.Any(u => u.UserName == user))
            {
                var newUser = new ApplicationUser()
                {
                    UserName = user,
                    Email = user,
                    FirstName = userFirstName,
                    LastName = userLastName
                };

                IdentityResult result = userManager.Create(newUser, password);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    userManager.AddToRole(newUser.Id, userRole);
                    context.SaveChanges();
                }
            }
        }
    }
}
