using internPlatform.Domain.Entities;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internPlatform.Application.Services.Statistics
{

    public class ErrorsService : IErrorsService
    {
        private readonly IRepository<ErrorLog> _errorsRepository;

        public ErrorsService(IRepository<ErrorLog> errorsRepository)
        {
            _errorsRepository = errorsRepository;
        }



        public async Task<PaginatedList<BriefErrorViewModel>> GetPaginatedTableAsync(int draw, int start, int length, string searchValue, int sortColumnIndex, string sortDirection)
        {
            IQueryable<ErrorLog> query = _errorsRepository.GetAll();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(e =>
                    e.Timestamp.Contains(searchValue) ||
                    e.CallSite.Contains(searchValue) ||
                    e.Message.Contains(searchValue)
                );
            }
            switch (sortColumnIndex)
            {
                case 0:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.Timestamp) : query.OrderByDescending(e => e.Timestamp);
                    break;
                case 1:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.CallSite) : query.OrderByDescending(e => e.CallSite);
                    break;
                case 2:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.Message) : query.OrderByDescending(e => e.Message);
                    break;
                case 3:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.LineNumber) : query.OrderByDescending(e => e.LineNumber);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.ErrorLogId) : query.OrderByDescending(e => e.ErrorLogId);
                    break;
            }
            var options = new PaginationOptions
            {
                PageSize = length,
                CurrentPage = (start / length) + 1
            };
            PaginatedList<ErrorLog> paginatedErrors = await _errorsRepository.GetPaginatedAsync(query, options);
            List<BriefErrorViewModel> errorDTOList = paginatedErrors.Select(e => new BriefErrorViewModel
            {
                ErrorLogId = e.ErrorLogId,
                Timestamp = e.Timestamp,
                Message = e.Message,
                CallSite = e.CallSite,
                LineNumber = e.LineNumber,
            }).ToList();
            return new PaginatedList<BriefErrorViewModel>(errorDTOList, paginatedErrors.TotalCount, paginatedErrors.CurrentPage, options.PageSize);
        }

        public async Task<ErrorViewModel> GetError(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var errorLog = await _errorsRepository.GetById(id);
            return new ErrorViewModel
            {
                ErrorLogId = errorLog.ErrorLogId,
                Timestamp = errorLog.Timestamp,
                Message = errorLog.Message,
                CallSite = errorLog.CallSite,
                LineNumber = errorLog.LineNumber,
                Level = errorLog.Level,
                Logger = errorLog.Logger,
                Exception = errorLog.Exception
            };
        }

        public async Task<bool> DeleteError(int id)
        {
            if (id != 0)
            {
                var entity = await _errorsRepository.GetById(id);
                if (entity != null)
                {
                    _errorsRepository.Remove(entity);
                    await _errorsRepository.Save();
                    return true;
                }
            }
            return false;
        }

    }
}
