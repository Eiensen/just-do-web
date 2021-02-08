﻿using JustDo_Web.Helpers;
using JustDo_Web.Models;
using JustDo_Web.ServerApp.Services.Validators;
using JustDo_Web.Services.Jwt;
using JustDo_Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
                var error = ErrorHelper.AddError($"{model.Email} is incorrect email string!");

                return BadRequest(error);
            }

            if (model.Password != model.ConfirmPassword)
            {
                var error = ErrorHelper.AddError("The Password and the ConfirmPassword must match!");

                return BadRequest(error);
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var error = ErrorHelper.AddIdentityErrors(result);

                return BadRequest(error);
            }

            return Ok();
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
                var error = ErrorHelper.AddError($"{model.Email} is not registered");

                return BadRequest(error);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return Ok(new { Token = _jwtGenerator.CreateToken(user) });
            }
            else
            {
                var error = ErrorHelper.AddError("The password is incorrect.");

                return BadRequest(error);
            }
        }
    }
}
