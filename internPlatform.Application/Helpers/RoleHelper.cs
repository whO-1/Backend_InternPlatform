using internPlatform.Application.Services;
using System.Web.Mvc;

namespace internPlatform.Application.Helpers
{
    public static class RoleHelper
    {
        public static bool Authentificate(string userId, string roleName)
        {
            var userService = DependencyResolver.Current.GetService<IUserRoleService>();
            bool result = userService.Authorize(userId, roleName).GetAwaiter().GetResult();
            return result;
        }
    }
}