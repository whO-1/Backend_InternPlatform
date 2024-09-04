using internPlatform.Application.Services;
using internPlatform.Application.Services.FilesOperations;
using internPlatform.Application.Services.Statistics;
using internPlatform.Domain.Models.ViewModels;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace internPlatform.Web.Areas.Admin.Controllers
{
    [RequireHttps]
    [CustomAuthorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        private readonly IErrorsService _errorsService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEventManageService _eventService;
        private readonly IFileService _fileService;
        private readonly IImageEntityService _imageService;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();


        public DashboardController
            (
                IErrorsService errorsService,
                IUserRoleService userRoleService,
                IEventManageService eventService,
                IFileService fileService,
                IImageEntityService imageService
            )
        {
            _errorsService = errorsService;
            _userRoleService = userRoleService;
            _eventService = eventService;
            _fileService = fileService;
            _imageService = imageService;
        }


        public ActionResult Index()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, Index");
                return RedirectToAction("Error");
            }

        }

        public ActionResult Error()
        {
            return View();
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult ErrorsStatistics()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, ErrorsStatistics");
                return RedirectToAction("Error");
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Entities()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, Entities");
                return RedirectToAction("Error");
            }
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Links()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, Links");
                return RedirectToAction("Error");
            }
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult SuperAdminStatistics()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, SuperAdminStatistics");
                return RedirectToAction("Error");
            };
        }

        public ActionResult UserStatistics()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, UserStatistics");
                return RedirectToAction("Error");
            }
        }

        public ActionResult Events()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, Events");
                return RedirectToAction("Error");
            }
        }

        public async Task<JsonResult> EventsGetAll()
        {
            try
            {
                int draw = Convert.ToInt32(Request["draw"]);
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                int sortColumnIndex = Convert.ToInt32(Request["order[0][column]"]);
                string sortDirection = Request["order[0][dir]"]; // "asc" or "desc"
                var userName = User.Identity.GetUserName();

                Logger.Info($"All events accessed by {userName}");
                var eventDTOList = await _eventService.GetPaginatedTableAsync(User.IsInRole("SuperAdmin"), userName, draw, start, length, searchValue, sortColumnIndex, sortDirection);
                return Json(new
                {
                    draw = (int)draw, // Echo back the draw counter
                    recordsTotal = eventDTOList.TotalCount,
                    recordsFiltered = eventDTOList.TotalCount, // Adjust if filtering is applied
                    data = eventDTOList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, EventsGetAll");
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> EventUpSert(int? Id)
        {
            try
            {
                var model = await _eventService.GetEventModel(User.Identity.GetUserName(), Id);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, EventUpSert");
                return RedirectToAction("Error");
            }

        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> EventUpSert(EventUpSertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var removedImageNames = _eventService.GetRemovedImages(model.StoredImagesIds, model.Id);
                    _fileService.DeleteFiles(removedImageNames);
                    await _eventService.EventUpSert(model);
                    var imageNames = _eventService.GetImageNamesFromModel(model.InputImages);
                    _fileService.SaveFromBuffer(imageNames);

                    return RedirectToAction("Events");
                }
                _eventService.PopulateWithEntities(model);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, EventUpSert-->POST");
                return RedirectToAction("Error");
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> EventDelete(int Id)
        {
            try
            {
                await _eventService.RemoveEvent(Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, EventDelete-->POST");
            }
            return RedirectToAction("Events");

        }

        [System.Web.Mvc.HttpPost]
        public async Task<JsonResult> ImagesBuffer(HttpPostedFileBase file)
        {
            try
            {
                string name = _fileService.Buffer(file);
                await _imageService.Add(name);
                var result = await _imageService.Get(i => i.Name == name);
                return Json(new { data = result.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, ImagesBuffer-->POST");
                return Json(new { Error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> GetImage(int Id)
        {
            try
            {
                var result = await _imageService.Get(img => img.ImageId == Id);
                if (result != null)
                {
                    var path = _fileService.GetFilePath(result.Name);
                    if (!String.IsNullOrEmpty(path))
                    {
                        var ext = _fileService.GetFileExt(path);
                        return File(path, $"image/{ext}");
                    }

                }
                return HttpNotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, GetImage");
                return HttpNotFound();
            }
        }

        public async Task<JsonResult> UpdateImagesOrder([FromBody] int[] ids, int eventId)
        {
            try
            {
                await _imageService.UpdateImagesOrder(ids, eventId);
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, UpdateImagesOrder");
                return Json(new { Result = ex.Message, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> DeleteImage(int Id)
        {
            try
            {
                var result = await _imageService.Get(img => img.ImageId == Id);
                if (result != null)
                {
                    var filePath = _fileService.GetFilePath(result.Name);
                    await _imageService.Remove(Id);
                    _fileService.DeleteFile(filePath);
                    return new HttpStatusCodeResult(200);
                }
                return HttpNotFound();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, DeleteImage");
                return RedirectToAction("Error");
            }
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public ActionResult Users()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, Users");
                return RedirectToAction("Error");
            };
        }
        [CustomAuthorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                int draw = Convert.ToInt32(Request["draw"]);
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                int sortColumnIndex = Convert.ToInt32(Request["order[0][column]"]);
                string sortDirection = Request["order[0][dir]"]; // "asc" or "desc"
                var userName = User.Identity.GetUserName();

                var userspaginated = await _userRoleService.GetUsersPaginatedAsync(draw, start, length, searchValue, sortColumnIndex, sortDirection); ;
                return Json(new
                {
                    draw = (int)draw,
                    recordsTotal = userspaginated.TotalCount,
                    recordsFiltered = userspaginated.TotalCount,
                    data = userspaginated
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, GetUsers");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var model = new URUpdateViewModel
                {
                    User = await _userRoleService.PopulateUserWithRole(u => u.Id == id)
                };
                model.Roles = _userRoleService.GetAllRoles(model.User.Role);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, DeleteUser");
                return RedirectToAction("Error");
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> DeleteUser(URUpdateViewModel model)
        {
            try
            {
                var result = await _userRoleService.DeleteUser(model.User.User.Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, DeleteUser-->POST");
            }
            return RedirectToAction("Users");
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> LockUser(string id)
        {
            try
            {
                var result = await _userRoleService.GetUser(id);
                if (result != null)
                {
                    UserLockViewModel model = new UserLockViewModel
                    {
                        UserId = id,
                        UserName = result.UserName,
                        Role = result.Roles.ToString(),
                        Status = (await _userRoleService.IsLocked(id)) ? "Locked" : "Active",
                        EndDate = (result.LockoutEndDateUtc != null) ? result.LockoutEndDateUtc.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm") : "",
                        Today = DateTime.Now.ToString("yyyy-MM-ddTHH:mm")
                    };
                    return View(model);
                }
                return View("Users");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, LockUser");
                return RedirectToAction("Users");
            }
        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> LockUser(UserLockViewModel model)
        {
            try
            {
                var result = await _userRoleService.LockUser(model);
                if (result)
                {
                    return View("Users");
                }
                var user = await _userRoleService.GetUser(model.UserId);
                if (user != null)
                {
                    model.UserName = user.UserName;
                    model.Role = user.Roles.ToString();
                    model.Status = (await _userRoleService.IsLocked(model.UserId)) ? "Locked" : "Active";
                    model.EndDate = (user.LockoutEndDateUtc != null) ? user.LockoutEndDateUtc.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm") : "";
                    model.Today = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, LockUser-->POST");
                return RedirectToAction("Users");
            }


        }

        [CustomAuthorize(Roles = "SuperAdmin")]

        public async Task<ActionResult> UpdateUser(string id)
        {
            try
            {
                var model = new URUpdateViewModel
                {
                    User = await _userRoleService.PopulateUserWithRole(u => u.Id == id),
                };

                model.Roles = _userRoleService.GetAllRoles(model.User.Role);
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, UpdateUser");
                return RedirectToAction("Users");
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(URUpdateViewModel model)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, UpdateUser-->POST");
                return RedirectToAction("Users");
            }
        }

        public async Task<JsonResult> GetErrors(int Id = 0)
        {
            try
            {
                int draw = Convert.ToInt32(Request["draw"]);
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                int sortColumnIndex = Convert.ToInt32(Request["order[0][column]"]);
                string sortDirection = Request["order[0][dir]"]; // "asc" or "desc"

                if (User.IsInRole("SuperAdmin"))
                {
                    var errorsList = await _errorsService.GetPaginatedTableAsync(draw, start, length, searchValue, sortColumnIndex, sortDirection);
                    return Json(new
                    {
                        draw = (int)draw,
                        recordsTotal = errorsList.TotalCount,
                        recordsFiltered = errorsList.TotalCount,
                        data = errorsList
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "ERROR", Message = "Forbidden" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, EventsGetAll");
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> ErrorView(int Id = 0)
        {
            try
            {
                var model = await _errorsService.GetError(Id);
                if (model != null)
                {
                    return View(model);
                }
                return RedirectToAction("ErrorsStatistics");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, ErrorView");
                return RedirectToAction("ErrorsStatistics");
            }

        }


        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> ErrorDelete(int Id = 0)
        {
            try
            {
                await _errorsService.DeleteError(Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in DashboardController, ErrorDelete");

            }
            return RedirectToAction("ErrorsStatistics");
        }

    }
}