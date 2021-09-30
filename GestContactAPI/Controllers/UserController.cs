using BusinessLogicLayer.Data;
using BusinessLogicLayer.Interfaces;
using GestContactAPI.Models;
using GestContactAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserClientRepo _userService;

        public UserController(IUserClientRepo userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginForm form)
        {

            UserClient currentUser = _userService.Login(form.Email, form.Password);
            if(currentUser is null)
            {
                return BadRequest("Utilisateur null");
            }
            return Ok(currentUser);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_userService.EmailExists(form.Email))
            {
                return BadRequest("E-mail déjà utilisé");
            }

            _userService.Register(form.toBll());
            return Ok();
        }
    }
}
