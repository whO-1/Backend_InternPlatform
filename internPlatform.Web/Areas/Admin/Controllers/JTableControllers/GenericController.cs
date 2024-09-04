using internPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    [RequireHttps]
    [CustomAuthorize(Roles = "SuperAdmin,Admin")]
    public class GenericController<T, T_DTO> : Controller
        where T : class
        where T_DTO : class
    {
        protected readonly IEntityManageService<T, T_DTO> _service;
        public GenericController(IEntityManageService<T, T_DTO> service)
        {
            _service = service;
        }


        [HttpPost]
        public virtual JsonResult EntityList()
        {
            try
            {
                IEnumerable<T_DTO> result = _service.GetAll();
                return Json(new { Result = "OK", Records = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<JsonResult> CreateEntity(T_DTO entity)
        {
            try
            {
                T_DTO result = await _service.Add(entity);
                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<JsonResult> DeleteEntity(int Id)
        {
            try
            {
                await _service.Remove(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }


        [HttpPost]
        public virtual async Task<JsonResult> UpdateEntity(T category)
        {
            try
            {
                await _service.Update(category);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}