using ImplementCors.Base.Controllers;
using ImplementCors.Repository.Data;
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
    public class PersonsController : BaseController<Person,PersonRepository,string>
    {
        private readonly PersonRepository repository;

        public PersonsController(PersonRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        [HttpGet("GetPerson")]
        public async Task<JsonResult> GetPerson()
        {
            var result = await repository.GetPersonVM();
            return Json(result);
        }

        [HttpGet("GetByPersonNIK/{NIK}")]
        public async Task<JsonResult> GetByPersonNIK(string NIK)
        {
            var result = await repository.GetPersonVM(NIK);
            return Json(result);
        }

        [HttpPost("Register")]
        public JsonResult Register(PersonVM register)
        {
            var result = repository.Register(register);
            return Json(result);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
