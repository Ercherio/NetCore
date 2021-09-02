﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Model;
using NETCore.Repositoty.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person,PersonRepository, string>
    {
        private readonly PersonRepository repository;
        public PersonsController(PersonRepository repository): base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetPerson")]
        public ActionResult GetPerson()
        {
            var getPerson = repository.GetPersonVMs();
            if (getPerson != null)
            {
                var get = Ok(new
                {
                    status = HttpStatusCode.OK,
                    result = getPerson,
                    message = "Success"
                });
                return get;
            }
            else
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result = getPerson,
                    message = "Data Empty"
                });
                return get;
            }
        }

        [HttpGet("GetPerson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var getPerson = repository.GetPersonVMs(NIK);
            if (getPerson != null)
            {
                var get = Ok(new
                {
                    status = HttpStatusCode.OK,
                    result = getPerson,
                    message = "Success"
                });
                return get;
            }
            else 
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result =getPerson,
                    message = "Data Empty"
                });
                return get;
            }
        }

        [HttpPost("Register")]
        public ActionResult Insert(PersonVM personVM)
        {
            try
            {
                int output = repository.Insert(personVM);
                if (output == 100)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate email",
                        /*error = e*/
                    });
                }
                else if (output == 200)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate NIK",
                        /*error = e*/
                    });
                }
                else if (output == 300)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate Phone Number",
                        /*error = e*/
                    });
                }
                return Ok(new
                {
                    /*statusCode = StatusCode(200),*/
                    status = HttpStatusCode.OK,
                    message = "Registration Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Error duplicate data",
                    /*error = e*/
                });
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            int output = repository.Login(loginVM);
            if (output == 100)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Email not available",
                    /*error = e*/
                });
            }
            else if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong Password",
                    /*error = e*/
                });
            }
            return Ok(new
            {
                /*statusCode = StatusCode(200),*/
                status = HttpStatusCode.OK,
                message = "Success"
            });

        }
    }
}