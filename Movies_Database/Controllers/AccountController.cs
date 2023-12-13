using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies_Database.Models;
using Movies_Database.Services;

namespace Movies_Database.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromBody] DeleteUserDto dto)
        {
            var user = _accountService.Delete(dto);

            if (user is false)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
