using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace internPlatform.Application.Services
{
    public interface IUserRoleService
    {

        Task<IEnumerable<URManager>> PopulateUsersWithRoles();

        Task<URManager> PopulateUserWithRole(Expression<Func<ApplicationUser, bool>> filter);

        // Task<bool> UpdateUser(URManager model);


        List<SelectListItem> GetAllRoles(string userRole);

        Task<bool> UpdateRole(URManager model, string selectedRole);
        Task<bool> UpdatePassword(URManager model, string newPassword);

        Task<bool> Authorize(string userId, string userRole);
        Task<bool> IsLocked(string userId);
        Task<bool> LockUser(UserLockViewModel model);
        Task<bool> DeleteUser(string userId);

        Task<PaginatedList<URManager>> GetUsersPaginatedAsync(int draw, int start, int length, string searchValue, int sortColumnIndex, string sortDirection);
        Task<ApplicationUser> GetUser(string userId);
    }
}
