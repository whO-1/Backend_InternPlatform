using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;


namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class EntryTypeController : GenericController<EntryType, EntryTypeDTO>
    {
        public EntryTypeController(IEntityManageService<EntryType, EntryTypeDTO> service) : base(service)
        {
        }
    }
}