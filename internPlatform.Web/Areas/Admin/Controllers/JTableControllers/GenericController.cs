﻿using internPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class GenericController<T, T_DTO> : Controller
        where T : class
        where T_DTO : class
    {
        private readonly IEntityManageService<T, T_DTO> _service;
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
        public async Task<JsonResult> DeleteEntity(int Id)
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