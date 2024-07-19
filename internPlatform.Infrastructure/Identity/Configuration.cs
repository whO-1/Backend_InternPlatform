using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;


namespace internPlatform.Infrastructure.Identity
{
    internal sealed class Configuration : DbMigrationsConfiguration<internPlatform.Infrastructure.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Identity";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedRolesAndUsers(context);
        }

        private void SeedRolesAndUsers(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string[] roles = Constants.Constants.Roles;
            const string superAdminEmail = "admin@mail.com";
            const string superAdminPassword = "master";

            const string adminEmail = "user@mail.com";
            const string adminPassword = "userPass";

            //Add Roles
            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    var identityRole = new IdentityRole(role);
                    roleManager.Create(identityRole);
                }
            }

            //Add SuperAdmin account
            var superAdminUser = userManager.FindByName(superAdminEmail);
            if (superAdminUser == null)
            {
                superAdminUser = new ApplicationUser { UserName = superAdminEmail, Email = superAdminEmail, EmailConfirmed = true };
                var result = userManager.Create(superAdminUser, superAdminPassword);
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(superAdminUser.Id, false);
                }
            }

            var rolesForSuperAdminUser = userManager.GetRoles(superAdminUser.Id);
            if (!rolesForSuperAdminUser.Contains(roles[0]))
            {
                userManager.AddToRole(superAdminUser.Id, roles[0]);
            }


            //Add Admin accound
            var adminUser = userManager.FindByName(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = userManager.Create(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(adminUser.Id, false);
                }
                else
                {
                    // Console.WriteLine(result.Errors.First().ToString());
                }
            }

            var rolesForAdminUser = userManager.GetRoles(adminUser.Id);
            if (!rolesForAdminUser.Contains(roles[1]))
            {
                userManager.AddToRole(adminUser.Id, roles[1]);
            }
        }
    }
}
