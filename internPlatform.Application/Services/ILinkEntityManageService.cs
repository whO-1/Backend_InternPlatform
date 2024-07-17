using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface ILinkEntityManageService : IEntityManageService<Link, LinkDTO>
    {
        List<JTSelectListItem> GetOptions();
        Task<bool> ValidateNewLink(Link updatedLink);
    }
}
