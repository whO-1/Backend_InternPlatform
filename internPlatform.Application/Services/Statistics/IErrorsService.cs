using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using System.Threading.Tasks;

namespace internPlatform.Application.Services.Statistics
{
    public interface IErrorsService
    {
        Task<PaginatedList<BriefErrorViewModel>> GetPaginatedTableAsync(int draw, int start, int length, string searchValue, int sortColumnIndex, string sortDirection);
        Task<ErrorViewModel> GetError(int id);
        Task<bool> DeleteError(int id);
    }
}
