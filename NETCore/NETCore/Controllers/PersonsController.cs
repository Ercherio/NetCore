using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Base;
using NETCore.Context;
using NETCore.Model;
using NETCore.Repositoty.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        public IConfiguration configuration;
        private readonly PersonRepository repository;
        private readonly MyContext myContext;
        public PersonsController(PersonRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.myContext = myContext;
        }

        //[Authorize(Roles ="User")]
        [EnableCors("AllowAllOrigins")]
        [HttpGet("GetPerson")]
        public ActionResult GetPerson()
        {
            var getPerson = repository.GetPersonVMs();
            if (getPerson.Count() >0)
            {
                //var get = Ok(new
                //{
                //    status = HttpStatusCode.OK,
                //    result = getPerson,
                //    message = "Success to Display Data"
                //});
                //return get;
                return Ok(getPerson);
            }
            else
            {
                //var get = NotFound(new
                //{
                //    status = HttpStatusCode.NotFound,
                //    result = getPerson,
                //    message = "Data Empty"
                //});
                //return get;
                return NotFound(getPerson);
            }
        }
        [EnableCors("AllowAllOrigins")]
        //[Authorize]
        [HttpGet("GetPerson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var getPerson = repository.GetPersonVMs(NIK);
            if (getPerson != null)
            {
                //var get = Ok(new
                //{
                //    status = HttpStatusCode.OK,
                //    result = getPerson,
                //    message = "Success to Display Data"
                //});
                //return get;
                return Ok(getPerson);
            }
            else 
            {
                //var get = NotFound(new
                //{
                //    status = HttpStatusCode.NotFound,
                //    result =getPerson,
                //    message = "Data Empty"
                //});
                //return get;
                return NotFound(getPerson);
            }
        }
        [EnableCors("AllowAllOrigins")]
        [HttpPost("Register")]
        public ActionResult Insert(PersonVM personVM)
        {
            try
            {
                int output = 0;
                if (ModelState.IsValid)
                {

                     output = repository.Insert(personVM);
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Check Format",
                        /*error = e*/
                    });
                }
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

       

        [AllowAnonymous]
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

            //var  claims = new List<Claim>();

            var data = (from p in myContext.Persons
                       join a in myContext.Accounts on
                       p.NIK equals a.NIK
                       join ar in myContext.AccountRoles on
                       a.NIK equals ar.AccountId
                       join r in myContext.Roles on
                       ar.RoleId equals r.Id
                       where p.Email == $"{loginVM.Email}"
                       select new PayloadVM
                       {
                           NIK = p.NIK,
                           Email = p.Email,
                           RoleName = r.Name
                       }).ToList();

            //var claims = new List<Claim>();
            //claims.Add(new Claim("NIK", data[0].NIK));
            //claims.Add(new Claim("Email", data[0].Email));
            //foreach (var d in data)
            //{
            //    claims.Add(new Claim("Role", d.RoleName));
            //}

            //var claims = new[]
            //{
            //        //new Claim(ClaimTypes.Role, getRole.Name),
            //        //new Claim(ClaimTypes.Role, getRole.RoleName),
            //        new Claim("NIK", getUserData.NIK),
            //        new Claim("Email", getUserData.Email),
            //        new Claim("Role Name", getRole.RoleName)
            //    };

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]));
            identity.AddClaim(new Claim("email", loginVM.Email));
            foreach (var item in data)
            {
                identity.AddClaim(new Claim("role", item.RoleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], 
                configuration["Jwt:Audience"], 
                identity.Claims, 
                expires: DateTime.UtcNow.AddDays(1), 
                signingCredentials: signIn);

            var idToken= new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                /*statusCode = StatusCode(200),*/
                status = HttpStatusCode.OK,
                token = idToken,
                message = "Login Success"
            });

        }
    }
}
