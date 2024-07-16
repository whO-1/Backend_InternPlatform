using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using internPlatform.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;





namespace internPlatform.Application.Services
{
    public class UserRoleService:IUserRoleService
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

        public async Task<bool> DeleteUser(string userId) 
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            if ( result == Microsoft.AspNet.Identity.IdentityResult.Success) 
            {
                return true;
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
            if ( hasPassword )
            {
                return true;
            }
            return false;
        }

        public  async Task<IEnumerable<URManager>> PopulateUsersWithRoles() 
        {
            List<ApplicationUser> list = _userManager.Users.ToList();
            List<URManager> usersWithRoles = new List<URManager>();
            foreach (var user in list)
            {
                var roleName = await _userManager.GetRolesAsync( user.Id );
                usersWithRoles.Add( new URManager { User = user, Role = roleName.FirstOrDefault() });
            }

            return usersWithRoles;
        }

        public async Task<URManager> PopulateUserWithRole(Expression<Func<ApplicationUser, bool>> filter)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;
            query = query.Where(filter);
            var user = query.FirstOrDefault();
            var roleName = await _userManager.GetRolesAsync( query.FirstOrDefault().Id );
           
            URManager userWithRole = new URManager { User = user, Role  =  roleName.FirstOrDefault() };

            return userWithRole;
        }

        public async Task<bool> UpdateRole(URManager model, string selectedRole)
        {
            try
            {
                IList<string> roles = await _userManager.GetRolesAsync(model.User.Id);
                string[] rolesArr =  roles.ToArray();
                await _userManager.RemoveFromRolesAsync(model.User.Id, rolesArr);
                await _userManager.AddToRoleAsync(model.User.Id, selectedRole );
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }


        public List<SelectListItem> GetAllRoles(string userRole)
        {
            IQueryable<IdentityRole> roles = _roleManagement.Roles ;

            List<SelectListItem> selectListItems = roles.Select(role => new SelectListItem
            {
                Value = role.Id,
                Text = role.Name,
                Selected = (userRole == role.Name)? true : false
            }).ToList();
                

            return selectListItems;
        }


        public async Task<bool> Authorize(string userId,string userRole)
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