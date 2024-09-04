using System.Web.Http;
using System.Web.Http.Cors;

namespace internPlatform.Api.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    public class TestController : ApiController
    {

        public IHttpActionResult Get()
        {
            return Ok();
        }

    }
}
