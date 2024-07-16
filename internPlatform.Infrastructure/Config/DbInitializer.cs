using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
{
    protected override void Seed(ApplicationDbContext context)
    {
        InitializeIdentityForEF(context);
        base.Seed(context);
    }

    public static void InitializeIdentityForEF(ApplicationDbContext db)
    {
        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        

        const string name = "admin@mail.com";
        const string password = "master";
        const string roleName = "SuperAdmin";
        string[] roles = { "SuperAdmin", "Admin" };
        // Create Role Admin if it does not exist
        foreach (var item in roles)
        {
            if (!roleManager.RoleExists(item))
            {
                var role = new IdentityRole(item);
                roleManager.Create(role);
            }
        }
        
        var user = userManager.FindByName(name);
        if (user == null)
        {
            user = new ApplicationUser { UserName = name, Email = name };
            var result = userManager.Create(user, password);
            result = userManager.SetLockoutEnabled(user.Id, false);
        }

        // Add user admin to Role Admin if not already added
        var rolesForUser = userManager.GetRoles(user.Id);
        if (!rolesForUser.Contains(roleName))
        {
            var result = userManager.AddToRole(user.Id, roleName);
        }
    }
}
