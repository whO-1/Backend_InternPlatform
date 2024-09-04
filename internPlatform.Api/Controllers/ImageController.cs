using internPlatform.Application.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace internPlatform.Api.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        private readonly IApiService _apiService;
        public ImageController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IHttpActionResult> Get(int Id)
        {
            try
            {
                var ImageBase64String = await _apiService.GetImage(Id);
                if (string.IsNullOrEmpty(ImageBase64String))
                {
                    return NotFound();
                }
                return Ok(new { ImageData = ImageBase64String });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }
    }
}
