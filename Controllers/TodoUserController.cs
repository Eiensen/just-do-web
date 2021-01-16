using JustDo_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JustDo_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoUserController : ControllerBase
    {
        private UserManager<TodoUser> _userManager;
        private SignInManager<TodoUser> _signInManager;

        public TodoUserController(UserManager<TodoUser> userManager, SignInManager<TodoUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/TodoUser/Register
        public async Task<Object> Register(TodoUserModel model)
        {
            var todoUser = new TodoUser()
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

        [HttpPost]
        [Route("Login")]
        //[ValidateAntiForgeryToken]
        //POST : /api/TodoUser/Login
        public async Task<Object> Login(TodoUserModel model)
        {
            
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                //await _signInManager.SignInAsync(todoUser, false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
