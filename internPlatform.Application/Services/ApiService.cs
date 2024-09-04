using internPlatform.Application.Services.FilesOperations;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly IRepository<Link> _repositoryLinks;
        private readonly IRepository<Event> _repositoryEvents;
        private readonly IRepository<Faq> _repositoryFaqs;
        private readonly IRepository<Image> _repositoryImages;
        private readonly IFileService _fileService;

        public ApiService(IRepository<Link> repositoryLinks, IRepository<Event> repositoryEvents, IRepository<Faq> repositoryFaqs, IRepository<Image> repositoryImages, IFileService fileService)
        {
            _repositoryLinks = repositoryLinks;
            _repositoryEvents = repositoryEvents;
            _repositoryFaqs = repositoryFaqs;
            _repositoryImages = repositoryImages;
            _fileService = fileService;
        }

        public List<LinkDTO> GetLinks()
        {
            List<LinkDTO> linksCollection = new List<LinkDTO>();
            IEnumerable<Link> links = _repositoryLinks.GetAll();

            var en = links.GetEnumerator();
            while (en.MoveNext())
            {
                linksCollection.Add(new LinkDTO
                {
                    Id = en.Current.Id,
                    LinkTitle = en.Current.LinkTitle,
                    LinkUrl = en.Current.LinkUrl,
                    DisplayOrder = en.Current.DisplayOrder,
                    HeadId = en.Current.HeadId
                });
            }
            return linksCollection;
        }

        public async Task<ApiEventViewModel> GetEventById(int Id)
        {
            ApiEventViewModel eventDTO = new ApiEventViewModel();
            Event e = await _repositoryEvents.Get(el => el.EventId == Id, null);
            if (e != null)
            {
                var imagesList = _repositoryImages.GetAll(img => img.Event.EventId == e.EventId).OrderBy(img => img.DisplayOrder).Select(img => img.ImageId).ToList();
                eventDTO = new ApiEventViewModel
                {
                    Id = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    SpecialGuests = e.SpecialGuests,
                    AgeGroupId = e.AgeGroupId,
                    AgeGroupName = (e.Age != null) ? e.Age.Name : "",
                    EntryTypeId = e.EntryTypeId,
                    EntryTypeName = (e.Entry != null) ? e.Entry.Name : "",
                    SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                    TimeStamp = e.TimeStamp,
                    EventLocation = e.EventLocation,
                    EventImages = (imagesList != null) ? imagesList : new List<int>(),
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                };
            }

            return eventDTO;

        }

        public async Task<PaginatedList<ApiEventViewModel>> GetEventsPaginated(string body, string search = "", string filter = "")
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            DateTime dayAfterTomorrow = today.AddDays(2);
            DateTime startCurrentWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endCurrentWeek = startCurrentWeek.AddDays(7);
            DateTime startNextWeekend = startCurrentWeek.AddDays(5);
            DateTime endNextWeekend = startNextWeekend.AddDays(2);

            PaginationOptions options = new PaginationOptions();
            IQueryable<Event> query = _repositoryEvents.GetAll(null, includeProperties: "Entry,Age,Categories")
                .Where(e => e.IsDeleted != true)
                .OrderBy(e => e.EventId);

            if (!string.IsNullOrEmpty(body))
            {
                var allParams = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                if (allParams.ContainsKey("PaginationOptions"))
                {
                    options = JsonConvert.DeserializeObject<PaginationOptions>(allParams["PaginationOptions"].ToString());
                }


                if (!string.IsNullOrWhiteSpace(search))
                {
                    string searchTerm = search.ToLower();
                    query = query.Where(e => e.Title.ToLower().Contains(searchTerm)
                                           || e.Description.ToLower().Contains(searchTerm)
                                           || e.SpecialGuests.ToLower().Contains(searchTerm));
                }

                switch (filter)
                {
                    case "new":
                        query = query.OrderByDescending(e => e.StartDate);
                        break;
                    case "thisweekend":
                        query = query.Where(e => e.StartDate >= startNextWeekend && e.StartDate < endNextWeekend);
                        break;
                    case "today":
                        query = query.Where(e => e.StartDate >= today && e.StartDate < tomorrow);
                        break;
                    case "tomorrow":
                        query = query.Where(e => e.StartDate >= tomorrow && e.StartDate < dayAfterTomorrow);
                        break;
                    case "thisweek":
                        query = query.Where(e => e.StartDate >= startCurrentWeek && e.StartDate < endCurrentWeek);
                        break;
                    default:
                        break;
                }
            }

            PaginatedList<Event> paginatedEvents = await _repositoryEvents.GetPaginatedAsync(query, options);
            List<ApiEventViewModel> eventDTOs = paginatedEvents.Select(e => new ApiEventViewModel
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                SpecialGuests = e.SpecialGuests,
                AgeGroupId = e.AgeGroupId,
                AgeGroupName = e.Age?.Name ?? "",
                EntryTypeId = e.EntryTypeId,
                EntryTypeName = e.Entry?.Name ?? "",
                SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                EventImages = e.Images.OrderBy(img => img.DisplayOrder).Select(x => x.ImageId).ToList(),
                TimeStamp = e.TimeStamp,
                EventLocation = e.EventLocation,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
            }).ToList();

            return new PaginatedList<ApiEventViewModel>(eventDTOs, paginatedEvents.TotalCount, paginatedEvents.CurrentPage, options.PageSize);
        }

        public List<FaqDTO> GetFaqs()
        {
            List<FaqDTO> faqsCollection = new List<FaqDTO>();
            IEnumerable<Faq> faqs = _repositoryFaqs.GetAll();

            var en = faqs.GetEnumerator();
            while (en.MoveNext())
            {
                faqsCollection.Add(new FaqDTO
                {
                    FaqId = en.Current.FaqId,
                    Title = en.Current.Title,
                    Description = en.Current.Description,
                });
            }
            return faqsCollection;
        }


        public async Task<string> GetImage(int Id)
        {
            var result = await _repositoryImages.GetById(Id);
            if (result != null)
            {
                var path = _fileService.GetFilePath(result.Name);
                var imgBytes = _fileService.GetFileAsBytes(path);
                var ImageString = _fileService.ConvertBytesToBase64(imgBytes);
                return ImageString;
            }
            return string.Empty;
        }

    }
}
