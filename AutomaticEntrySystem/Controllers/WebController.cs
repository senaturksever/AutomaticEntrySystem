using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutomaticEntrySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WebController : ControllerBase
    {

        [HttpGet]
        [Route("webHomePage", Name = "webHomePage")]
        public ActionResult WebMobilePage()
        {
            return Ok("Web Sitesine Giriş Yapıldı.");
        }
    }
}
