using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class LinkController : GenericController<Link>
    {
        private readonly ILinkEntityManageService _service;
        public LinkController(ILinkEntityManageService service ):base(service) 
        {
            _service = service; 
        }



        [HttpPost]
        public JsonResult GetOptions()
        {
            try
            {
                var options = _service.GetOptions();
                return Json(new { Result = "OK" , Options = options }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public override async Task<JsonResult> UpdateEntity(Link updatedLink)
        {
            try
            {
                bool result = await _service.ValidateNewLink(updatedLink);
                if (result)
                {
                    _service.Update(updatedLink);
                    await _service.Save();
                    return Json(new { Result = "OK" });
                }
                return Json(new { Result = "ERROR", Message = "Not valid Entry" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }
    }
}