using AutomaticEntrySystem.Dtos;
using AutomaticEntrySystem.Dtos.LoginDto;
using AutomaticEntrySystem.Dtos.RegisterDto;
using AutomaticEntrySystem.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AutomaticEntrySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobileController : ControllerBase
    {
        private MobileManager manager;
        public MobileController(MobileManager mobileManager)
        {
            manager = mobileManager;
        }
        [HttpPost]
        [Route("register")]
        public RegisterResponseDto Register([FromForm]RegisterRequestDto registerDto)
        {
            var result = manager.Register(registerDto);
            return result;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponseDto> Register([FromBody]LoginRequestDto loginDto)
        {
            var result = manager.Login(loginDto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
