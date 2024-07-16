using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Data;
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

        Task <IEnumerable<URManager>> PopulateUsersWithRoles();

        Task<URManager> PopulateUserWithRole(Expression<Func<ApplicationUser, bool>> filter);

       // Task<bool> UpdateUser(URManager model);


        List<SelectListItem> GetAllRoles(string userRole);

        Task<bool> UpdateRole(URManager model, string selectedRole);
        Task<bool> UpdatePassword(URManager model, string newPassword);

        Task<bool> Authorize(string userId,string userRole);

        Task<bool> DeleteUser(string userId);
    }
}
