
using internPlatform.Application.Services;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models.ViewModels;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace internPlatform.Web.Areas.Admin.Controllers
{
    [RequireHttps]
    [CustomAuthorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRoleService _userRoleService;
        private readonly IEventManageService _eventService;
        public DashboardController
            (IUserRoleService userRoleService, IEventManageService eventService)
        {
            _userRoleService = userRoleService;
            _eventService = eventService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Entities()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Links()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Orders()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin,Admin")]
        public ActionResult Events()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin,Admin")]
        public JsonResult EventsGetAll()
        {
            Logger.Info($"All events accessed by {User.Identity.GetUserName()}");
            List<EventDTO> eventDTOList = _eventService.GetAllEvents(User.IsInRole("SuperAdmin"), User.Identity.GetUserName());
            return Json(new { data = eventDTOList }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> EventUpSert(int? Id)
        {
            var model = await _eventService.GetEventModel(User.Identity.GetUserName(), Id);
            return View(model);
        }

        [CustomAuthorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<ActionResult> EventUpSert(EventUpSertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _eventService.EventUpSert(model);
                    return RedirectToAction("Events");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Debug.WriteLine(ex.Message);
            }
            _eventService.PopulateWithEntities(model);
            return View(model);
        }

        [CustomAuthorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<ActionResult> EventDelete(int Id)
        {
            try
            {
                await _eventService.RemoveEvent(Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return RedirectToAction("Events");

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Users()
        {
            var Users = await _userRoleService.PopulateUsersWithRoles();
            return View(Users);
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var model = new URUpdateViewModel
            {
                User = await _userRoleService.PopulateUserWithRole(u => u.Id == id)
            };
            model.Roles = _userRoleService.GetAllRoles(model.User.Role);
            return View(model);
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [RequireHttps]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(URUpdateViewModel model)
        {
            var result = await _userRoleService.DeleteUser(model.User.User.Id);
            return RedirectToAction("Users");
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> UpdateUser(string id)
        {
            var model = new URUpdateViewModel
            {
                User = await _userRoleService.PopulateUserWithRole(u => u.Id == id),
            };

            model.Roles = _userRoleService.GetAllRoles(model.User.Role);
            return View(model);
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [RequireHttps]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(URUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.User.Role != model.SelectedRole.ToString())
                {
                    await _userRoleService.UpdateRole(model.User, model.SelectedRole);
                }
                if (model.NewPassword != null)
                {
                    var result = await _userRoleService.UpdatePassword(model.User, model.NewPassword);
                }
                return RedirectToAction("Users");
            }

            model.Roles = _userRoleService.GetAllRoles(model.User.Role);
            return View(model);
        }

    }
}