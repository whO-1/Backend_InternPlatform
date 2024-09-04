using internPlatform.Application.Services;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace internPlatform.Api.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class EventsController : ApiController
    {
        private readonly IApiService _apiService;
        public EventsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET api/values/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var Event = await _apiService.GetEventById(id);
                var response = new { Result = "OK", Records = Event };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

        // POST api/values
        public async Task<IHttpActionResult> Post(
            [FromUri] string search = null,
            [FromUri] string filter = null
        )
        {
            try
            {
                string requestBody = await Request.Content.ReadAsStringAsync();
                PaginatedList<ApiEventViewModel> Events = await _apiService.GetEventsPaginated(requestBody, search, filter);
                var response = new { Result = "OK", Records = Events, Events.TotalPages, Events.CurrentPage, Events.HasNextPage, Events.HasPreviousPage };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
