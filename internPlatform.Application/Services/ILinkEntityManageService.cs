using internPlatform.Domain.Entities;
using internPlatform.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface ILinkEntityManageService : IEntityManageService<Link>
    {
        List<JTSelectListItem> GetOptions();
        Task<bool> ValidateNewLink(Link updatedLink);
    }
}
