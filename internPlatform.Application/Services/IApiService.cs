using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IApiService
    {
        List<LinkDTO> GetLinks();
        Task<ApiEventViewModel> GetEventById(int Id);
        Task<PaginatedList<ApiEventViewModel>> GetEventsPaginated(string body, string search = "", string filter = "");
        List<FaqDTO> GetFaqs();
        Task<string> GetImage(int Id);
    }
}
