using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;





namespace internPlatform.Application.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManagement _roleManagement;
        private readonly IOwinContext context;

        public UserRoleService(IOwinContext context)
        {
            this.context = context;
            _userManager = context.GetUserManager<ApplicationUserManager>();
            _roleManagement = context.Get<ApplicationRoleManagement>();
        }

        public async Task<bool> IsLocked(string userId)
        {
            if (await _userManager.IsLockedOutAsync(userId) && await _userManager.GetLockoutEndDateAsync(userId) >= DateTimeOffset.UtcNow)
            {
                return true;
            }
            return false;
        }
        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<bool> LockUser(UserLockViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                bool check = await IsLocked(model.UserId);
                if (!check)
                {
                    DateTime parsedDateTime = DateTime.Parse(model.EndDate);
                    DateTimeOffset LockoutEndDate = new DateTimeOffset(parsedDateTime);
                    var result1 = await _userManager.SetLockoutEnabledAsync(model.UserId, true);
                    var result2 = await _userManager.SetLockoutEndDateAsync(model.UserId, parsedDateTime);
                    if (result1 == result2 && result1 == Microsoft.AspNet.Identity.IdentityResult.Success)
                    {
                        return true;
                    }
                }
                var result3 = await _userManager.SetLockoutEnabledAsync(model.UserId, false);
                if (result3 == Microsoft.AspNet.Identity.IdentityResult.Success)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result == Microsoft.AspNet.Identity.IdentityResult.Success)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdatePassword(URManager model, string newPassword)
        {
            var id = model.User.Id;
            var hasPassword = await _userManager.HasPasswordAsync(id);

            if (hasPassword == true)
            {
                await _userManager.RemovePasswordAsync(id);
                await _userManager.AddPasswordAsync(id, newPassword);
            }
            hasPassword = await _userManager.HasPasswordAsync(id);
            if (hasPassword)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<URManager>> PopulateUsersWithRoles()
        {
            List<ApplicationUser> list = _userManager.Users.ToList();
            List<URManager> usersWithRoles = new List<URManager>();
            foreach (var user in list)
            {
                var roleName = await _userManager.GetRolesAsync(user.Id);
                usersWithRoles.Add(new URManager { User = user, Role = roleName.FirstOrDefault() });
            }

            return usersWithRoles;
        }


        public async Task<PaginatedList<URManager>> GetUsersPaginatedAsync(int draw, int start, int length, string searchValue, int sortColumnIndex, string sortDirection)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                usersQuery = usersQuery.Where(e =>
                    e.UserName.Contains(searchValue)
                );
            }

            switch (sortColumnIndex)
            {
                case 0:
                    usersQuery = sortDirection == "asc" ? usersQuery.OrderBy(e => e.UserName) : usersQuery.OrderByDescending(e => e.UserName);
                    break;
                default:
                    usersQuery = sortDirection == "asc" ? usersQuery.OrderBy(e => e.UserName) : usersQuery.OrderByDescending(e => e.UserName);
                    break;
            }

            var options = new PaginationOptions
            {
                PageSize = length,
                CurrentPage = (start / length) + 1
            };
            int totalCount = usersQuery.Count();
            var items = usersQuery.Skip((options.CurrentPage - 1) * options.PageSize)
                                       .Take(options.PageSize)
                                       .ToList();
            PaginatedList<ApplicationUser> paginatedUsers = new PaginatedList<ApplicationUser>(items, totalCount, options.CurrentPage, options.PageSize);
            List<URManager> usersWithRoles = new List<URManager>();
            foreach (var user in paginatedUsers)
            {
                var roleNames = await _userManager.GetRolesAsync(user.Id);
                var roleName = roleNames.FirstOrDefault(); // Get the first role or null
                usersWithRoles.Add(new URManager
                {
                    User = user,
                    Role = roleName
                });
            }

            return new PaginatedList<URManager>(usersWithRoles, totalCount, options.CurrentPage, options.PageSize);
        }





        public async Task<URManager> PopulateUserWithRole(Expression<Func<ApplicationUser, bool>> filter)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;
            query = query.Where(filter);
            var user = query.FirstOrDefault();
            var roleName = await _userManager.GetRolesAsync(query.FirstOrDefault().Id);

            URManager userWithRole = new URManager { User = user, Role = roleName.FirstOrDefault() };

            return userWithRole;
        }

        public async Task<bool> UpdateRole(URManager model, string selectedRole)
        {
            try
            {
                IList<string> roles = await _userManager.GetRolesAsync(model.User.Id);
                string[] rolesArr = roles.ToArray();
                await _userManager.RemoveFromRolesAsync(model.User.Id, rolesArr);
                await _userManager.AddToRoleAsync(model.User.Id, selectedRole);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<SelectListItem> GetAllRoles(string userRole)
        {
            IQueryable<IdentityRole> roles = _roleManagement.Roles;

            List<SelectListItem> selectListItems = roles.Select(role => new SelectListItem
            {
                Value = role.Id,
                Text = role.Name,
                Selected = (userRole == role.Name) ? true : false
            }).ToList();


            return selectListItems;
        }


        public async Task<bool> Authorize(string userId, string userRole)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                bool adminRole = _userManager.IsInRole(user.Id, userRole);

                if (adminRole)
                {
                    return true;
                }
            }
            return false;
        }


    }
}