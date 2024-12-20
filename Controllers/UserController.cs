using Microsoft.AspNetCore.Mvc;
using Motus.API.Core.Services.SignUpService;

namespace Motus.API.Controllers
{
    public class UserController : Controller
    {
        private ISignUpService SignUpService;

        public UserController(ISignUpService signup) {
            SignUpService = signup;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(string firstname, string lastname, string email, string data, string phonenumber, string password) { 
            SignUpService.AddUser(firstname, lastname, email, data, phonenumber, password);
            return Ok(); 
        }
        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(string email)
        {
            SignUpService.DeleteUser(email);
            return Ok();
        }

        [HttpPost("ActivateUser")]
        public IActionResult ActivateUser(string email, string token) { 
            SignUpService.UserActivation(email, token);
            return Ok();
        }
    }
}
