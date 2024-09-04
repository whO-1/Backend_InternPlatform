using internPlatform.Application.Services;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Web.Areas.Admin.Controllers.JTableControllers
{
    public class FaqController : GenericController<Faq, FaqDTO>
    {
        public FaqController(IEntityManageService<Faq, FaqDTO> service) : base(service)
        {
        }
    }
}