using JustDo_Web.Interfaces;
using JustDo_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JustDo_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IJwtGenerator _jwtGenerator;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> Register(UserModel model)
        {
            var todoUser = new User()
            {
                Email = model.Email,
                UserName = model.Email               
            };
            try
            {
                var result = await _userManager.CreateAsync(todoUser, model.Password);

                //await _signInManager.SignInAsync(todoUser, false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]       
        //POST : /api/User/Login
        public async Task<ActionResult/*<UserModel>*/> Login([FromBody]UserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                //return new UserModel
                //{
                //    Email = user.Email,
                //    Token = _jwtGenerator.CreateToken(user)
                //};
                return Ok(new { Token = _jwtGenerator.CreateToken(user)});
            }

            return Unauthorized();            
        }

    }
}
