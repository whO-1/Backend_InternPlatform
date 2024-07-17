using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IApiService
    {
        List<LinkDTO> GetLinks();
        List<EventDTO> GetEvents();
        Task<EventDTO> GetEventById(int Id);
        Task<PaginatedList<EventDTO>> GetEventsPaginated(string body);
    }
}
