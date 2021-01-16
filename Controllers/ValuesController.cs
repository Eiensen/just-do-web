using JustDo_Web.Interfaces;
using JustDo_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JustDo_Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IJwtAuthManager jwtAuthManager;

        public ValuesController(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateToken([FromBody] TodoUserModel model)
        {
            var token = jwtAuthManager.Authenticate(model.Email, model.Password);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
      
    }
}
