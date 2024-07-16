using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class EntryTypeController : GenericController<EntryType>
    {
        public EntryTypeController(IEntityManageService<EntryType> service) : base(service)
        {
        }
    }
}