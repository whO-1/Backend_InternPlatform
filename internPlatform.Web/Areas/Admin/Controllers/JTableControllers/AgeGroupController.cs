using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using internPlatform.Application.Services;
using internPlatform.Domain.Entities;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class AgeGroupController : GenericController<AgeGroup>
    {
        public AgeGroupController( IEntityManageService<AgeGroup> service ):base(service) 
        {
        }
    }
}