using internPlatform.Application.Services;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace internPlatform.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class ApiController : Controller
    {
        private readonly IApiService _apiService;
        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }



        public JsonResult Test()
        {
            try
            {

                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> Events(int Id = 0)
        {
            try
            {
                if (Id != 0)
                {
                    var Event = await _apiService.GetEventById(Id);
                    return Json(new { Result = "OK", Records = Event }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string body;
                    using (var reader = new StreamReader(Request.InputStream))
                    {
                        body = reader.ReadToEnd();
                    }

                    PaginatedList<ApiEventViewModel> Events = await _apiService.GetEventsPaginated(body);
                    return Json(new { Result = "OK", Records = Events, Events.TotalPages, Events.CurrentPage, Events.HasNextPage, Events.HasPreviousPage }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}