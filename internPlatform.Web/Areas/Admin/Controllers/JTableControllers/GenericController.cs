using internPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class GenericController <T> : Controller where T : class 
    {
        private readonly IEntityManageService<T> _service;
        public GenericController(IEntityManageService<T> service )
        {
            _service = service;
        }


        [HttpPost]
        public virtual JsonResult EntityList()
        {
            try
            {
                IEnumerable<T> result = _service.GetAll();
                return Json(new { Result = "OK", Records = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<JsonResult> CreateEntity(T entity)
        {
            try
            {
                T result = _service.Add(entity);
                await _service.Save();
                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public  async Task<JsonResult> DeleteEntity(int Id)
        {
            try
            {
                T result = await _service.Remove(Id);
                await _service.Save();
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
                _service.Update(category);
                await _service.Save();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}