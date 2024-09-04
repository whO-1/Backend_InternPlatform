using internPlatform.Application.Services;
using System;
using System.Collections;
using System.Web.Http;
using System.Web.Http.Cors;

namespace internPlatform.Api.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class LinksController : ApiController
    {
        private readonly IApiService _apiService;
        public LinksController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable links = _apiService.GetLinks();
                var response = new { Result = "OK", Records = links };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
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
