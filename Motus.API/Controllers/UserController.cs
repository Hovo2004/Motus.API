using Microsoft.AspNetCore.Mvc;
using Motus.API.Core.Services.SignUpService;

namespace Motus.API.Controllers
{
    public class UserController : Controller
    {
        public ISignUpService SignUpService;

        public UserController(ISignUpService signup) {
            SignUpService = signup;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser() { return Ok(); }

        [HttpPost("ActivateUser")]
        public IActionResult EditUser(string email) { 
            SignUpService.UserActivation(email);
            return Ok();
        }
    }
}
