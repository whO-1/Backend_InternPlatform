using internPlatform.Application.Services;
using internPlatform.Domain.Entities;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{ 
    public class CategoryController : GenericController<Category>
    {
        public CategoryController(IEntityManageService<Category> service):base(service) 
        {   
        }

    }
}