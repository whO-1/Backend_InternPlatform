using internPlatform.Domain.Entities;
using internPlatform.Infrastructure.Repository.IRepository;
using System.Collections.Generic;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace internPlatform.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly IRepository<Link> _repositoryLinks;
        private readonly IRepository<Event> _repositoryEvents;

        public ApiService(IRepository<Link> repositoryLinks, IRepository<Event> repositoryEvents)
        {
            _repositoryLinks = repositoryLinks;  
            _repositoryEvents = repositoryEvents;
        }

        public List<LinkDTO> GetLinks()
        {
            List<LinkDTO> linksCollection = new List<LinkDTO>();
            IEnumerable<Link> links = _repositoryLinks.GetAll();

            var en = links.GetEnumerator();
            while (en.MoveNext()) {
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

        public List<EventDTO> GetEvents()
        {
            List<EventDTO> eventDTOList = new List<EventDTO>();
            IEnumerable<Event> events = _repositoryEvents.GetAll(includeProperties: "Entry,Age").ToList();
            var en = events.GetEnumerator();

            while (en.MoveNext() )
            {
                eventDTOList.Add(new EventDTO
                {
                    Id = en.Current.EventId,
                    Title = en.Current.Title,
                    Description = en.Current.Description,
                    Age = en.Current.Age,
                    Entry = en.Current.Entry,
                    Categories = en.Current.Categories.Select(ec => ec.Name).ToList(),
                    AuthorId = en.Current.AuthorId,
                    SpecialGuests = en.Current.SpecialGuests,
                    TimeStamp = en.Current.TimeStamp,
                    StartDate = en.Current.StartDate,
                    EndDate = en.Current.EndDate,
                });
            }

            return eventDTOList;
        }

        public async Task<EventDTO> GetEventById(int Id)
        {
            EventDTO eventDTO= new EventDTO();
            Event Event = await _repositoryEvents.Get(e => e.EventId == Id, null);

            eventDTO = new EventDTO
            {
                Id = Event.EventId,
                Title = Event.Title,
                Description = Event.Description,
                Age = Event.Age,
                Entry = Event.Entry,
                Categories = Event.Categories.Select(ec => ec.Name).ToList(),
                AuthorId = Event.AuthorId,
                SpecialGuests = Event.SpecialGuests,
                TimeStamp = Event.TimeStamp,
                StartDate = Event.StartDate,
                EndDate = Event.EndDate,
            };
            return eventDTO;
        }

        public async Task<PaginatedList<EventDTO>> GetEventsPaginated( string body)
        {
            PaginationOptions options =  JsonConvert.DeserializeObject<PaginationOptions>(body);
            if(options == null)
            {
                options = new PaginationOptions();
            }
            
            PaginatedList<Event> events = await _repositoryEvents.GetPaginatedAsync(options, x => x.EventId );
            if(events != null)
            {
                List<EventDTO> eventDTOs = events.Select( e => new EventDTO
                {
                    Id = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    Age = e.Age,
                    Entry = e.Entry,
                    Categories = e.Categories.Select(ec => ec.Name).ToList(),
                    AuthorId = e.AuthorId,
                    SpecialGuests = e.SpecialGuests,
                    TimeStamp = e.TimeStamp,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                }).ToList() ;
                PaginatedList<EventDTO> paginatedEventDTOs = new PaginatedList<EventDTO>(eventDTOs, events.TotalCount, events.CurrentPage, options.PageSize);
                return paginatedEventDTOs;
            }

            return null;
        }

    }
}
