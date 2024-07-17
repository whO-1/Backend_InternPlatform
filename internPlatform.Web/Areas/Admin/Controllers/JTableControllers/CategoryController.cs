using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class CategoryController : GenericController<Category, CategoryDTO>
    {
        public CategoryController(IEntityManageService<Category, CategoryDTO> service) : base(service)
        {
        }

    }
}