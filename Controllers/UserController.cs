using JustDo_Web.Models;
using JustDo_Web.ServerApp.Services.Validators;
using JustDo_Web.Services.Jwt;
using JustDo_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JustDo_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtGenerator;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<ActionResult> Register(UserRegistration model)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!EmailValidator.IsValidEmail(model.Email))
            {
                return BadRequest($"{model.Email} is incorrect email string!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("The Password and the ConfirmPassword must match!");
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        //POST : /api/User/Login
        public async Task<ActionResult> Login([FromBody] UserLogin model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized($"{model.Email} is not registered");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return Ok(new { Token = _jwtGenerator.CreateToken(user) });
            }

            return Unauthorized();
        }
    }
}
