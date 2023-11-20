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
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);

            return Ok();
        }
    }
}
