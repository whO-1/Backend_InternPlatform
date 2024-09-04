using internPlatform.Application.Services;
using internPlatform.Application.Services.Statistics;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers
{
    //[CustomAuthorize(Roles = "SuperAdmin,Admin")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }


        [CustomAuthorize(Roles = "SuperAdmin")]
        public JsonResult StatisticUsersEvents()
        {
            try
            {
                var list = _statisticsService.CountUsersEvents();
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public JsonResult StatisticLastHalfYearPosts()
        {
            try
            {
                var list = _statisticsService.CountLastHalfYearPosts();
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public JsonResult StatisticPostsPerCategory()
        {
            try
            {
                var list = _statisticsService.CountPostsPerCategory();
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [CustomAuthorize(Roles = "SuperAdmin")]
        public JsonResult StatisticUsersInfo()
        {
            try
            {
                var list = _statisticsService.CountUsersInfo();
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult StatisticPostsInfo()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var list = _statisticsService.CountPostsInfo(userId);
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult StatisticRecentPosts()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var list = _statisticsService.CountRecentPosted(userId);
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult StatisticTopPosts()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var list = _statisticsService.CountTopPosts(userId);
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult StatisticLastHalfYearPostsUser()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var list = _statisticsService.CountLastHalfYearPostsUser(userId);
                return Json(new { Result = "OK", Records = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}