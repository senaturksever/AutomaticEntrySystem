using AutomaticEntrySystem.Dtos;
using AutomaticEntrySystem.Dtos.LoginDto;
using AutomaticEntrySystem.Dtos.MobilePageDto;
using AutomaticEntrySystem.Dtos.RegisterDto;
using AutomaticEntrySystem.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AutomaticEntrySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MobileController : ControllerBase
    {
        private MobileManager manager;
        public MobileController(MobileManager mobileManager)
        {
            manager = mobileManager;
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public RegisterResponseDto Register([FromForm]RegisterRequestDto registerDto)
        {
            var result = manager.Register(registerDto);
            return result;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponseDto> Register([FromBody]LoginRequestDto loginDto)
        {
            var result = manager.Login(loginDto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost]
        [Route("mobilPage")]
        public ActionResult<MobilePageResponse> MobileHomePage([FromBody]MobilePageRequest mobilePageRequest)
        {
            if (mobilePageRequest.isRouteWebPage)
            {
                string webLink = Url.Link("webHomePage", null);
                return Ok(new MobilePageResponse
                {
                    WebLink = webLink,
                    Status = true,
                    statusCode = 200,
                    StatusMessage = "Yönlendirme Link"
                });
            }
            return BadRequest(mobilePageRequest);
        }

    }
}
