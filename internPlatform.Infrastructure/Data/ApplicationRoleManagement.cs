using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;

namespace internPlatform.Infrastructure.Data
{
    public class ApplicationRoleManagement : RoleManager<IdentityRole>
    {
        public ApplicationRoleManagement(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManagement Create(IdentityFactoryOptions<ApplicationRoleManagement> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
            return new ApplicationRoleManagement(roleStore);
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var result = await this.CreateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

    }
}