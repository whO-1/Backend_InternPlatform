using internPlatform.Application.Services;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace internPlatform.Api.Controllers
{

    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class FaqsController : ApiController
    {
        private readonly IApiService _apiService;
        public FaqsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        //public async Task<IHttpActionResult> Get(int id)
        //{
        //    try
        //    {
        //        var Event = await _apiService.GetEventById(id);
        //        var response = new { Result = "OK", Records = Event };
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }

        //}

        public IHttpActionResult Get()
        {
            try
            {
                var faqs = _apiService.GetFaqs();
                var response = new { Result = "OK", Records = faqs };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }
    }
}
