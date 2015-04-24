using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("SecurityTest")]
    public class SecurityTestController : ApiController
    {
        [HttpGet, Route("Unprotected")]
        public string Unprotected()
        {
            return "Hello World!";
        }

        [HttpGet, Route("Protected"), Authorize]
        public string Protected()
        {
            return "Hello World!";
        }

        [HttpGet, Route("superprotected"), Authorize(Roles = "Administrator")]
        public string SuperProtected()
        {
            return "Hello World!";
        }

        [HttpGet, Route("secret"), Authorize(Roles = "Test")]
        public string Secret()
        {
            return "Hello World!";
        }
    }
}
