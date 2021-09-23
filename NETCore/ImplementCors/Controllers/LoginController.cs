using ImplementCors.Base.Controllers;
using ImplementCors.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Model;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController<Person, LoginRepository, string>
    {
        private readonly LoginRepository repository;

        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            var login = new LoginVM { 
                Email = email, Password = password 
            };
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

    }
}
