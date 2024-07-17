using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class AgeGroupController : GenericController<AgeGroup, AgeGroupDTO>
    {
        public AgeGroupController(IEntityManageService<AgeGroup, AgeGroupDTO> service) : base(service)
        {
        }
    }
}