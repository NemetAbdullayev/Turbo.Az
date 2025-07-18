using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft;
using Newtonsoft.Json;
using EntityLayer.DTOs.UserDtos;
using EntityLayer.Tables;
using BusinessLayer.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuth.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: auth/login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            if (String.IsNullOrEmpty(user.UserName))
            {
                return BadRequest(new { message = "Email address needs to entered" });
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Password needs to entered" });
            }

            

            try
            {
                UserList loggedInUser = await _authService.Login(user.UserName, user.Password);
                if (loggedInUser != null)
                {
                    
                    return Ok(loggedInUser);
                   // return Ok(JsonConvert.SerializeObject(loggedInUser));
                }
            }
            catch (Exception ex)
            {

                throw new Exception( ex.ToString());
            }

            return BadRequest(new { message = "User login unsuccessful" });
        }

        // POST: auth/register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            if (String.IsNullOrEmpty(user.Name))
            {
                return BadRequest(new { message = "Name needs to entered" });
            }
            else if (String.IsNullOrEmpty(user.UserName))
            {
                return BadRequest(new { message = "User name needs to entered" });
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Password needs to entered" });
            }

            User userToRegister = new(user.UserName, user.Name, user.Password, user.Role);

            User registeredUser = await _authService.Register(userToRegister);

            UserList loggedInUser = await _authService.Login(registeredUser.UserName, user.Password);

            if (loggedInUser != null)
            {
                return Ok(loggedInUser);
            }

            return BadRequest(new { message = "User registration unsuccessful" });
        }

      
        
    }
}
