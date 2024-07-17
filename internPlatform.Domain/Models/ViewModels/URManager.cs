
using internPlatform.Infrastructure.Interfaces;

namespace internPlatform.Domain.Models.ViewModels
{
    public class URManager
    {
        public ApplicationUser User { get; set; }

        public string Role { get; set; }

        public URManager() { }
        public URManager(ApplicationUser user, string role)
        {
            User = user;
            Role = role;

        }

    }
}