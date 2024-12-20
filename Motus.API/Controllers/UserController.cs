using Microsoft.AspNetCore.Mvc;
using Motus.API.Core.Services.SignUpService;

namespace Motus.API.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISignUpService _signUpService;

        public UserController(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(string firstname, string lastname, string email, DateTime data, string phonenumber, string password)
        {
            try
            {
                var x = _signUpService.AddUser(firstname, lastname, email, data, phonenumber, password);
                return Ok(x);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding user: {ex.Message}");
            }
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(string email)
        {
            try
            {
                var x = _signUpService.DeleteUser(email);
                return Ok(x);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting user: {ex.Message}");
            }
        }

        [HttpGet("ActivateUser")]
        public IActionResult ActivateUser(string email, string token)
        {
            try
            {
                _signUpService.UserActivation(email, token);
                return Ok("User activated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error activating user: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var isAuthenticated = _signUpService.login(email, password);
                return Ok(new { Success = isAuthenticated });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error during login: {ex.Message}");
            }
        }
    }
}