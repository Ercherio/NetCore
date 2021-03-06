using Microsoft.AspNetCore.Http;
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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository): base(repository)
        {
            this.repository = repository;

        }

        [HttpPost("forget-password")]
        public ActionResult ForgetPassword(EmailVM emailVM)
        {
            
            int output = repository.ForgetPassword(emailVM.Email);
         
            if (output == 100)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email/Account Not Found",
                    /*error = e*/
                });
            }
            return Ok(new
            {
                /*statusCode = StatusCode(200),*/
                status = HttpStatusCode.OK,
                message = "Reset Password has been change. Check your Email !"
            });
        }

        //[HttpPost("reset-password/email={Email}&token={Token}")]
        //public ActionResult ResetPassword(string Email, string Token)
        //{
        //    /*string tempEmail = Request.Query.Keys.Contains("email").ToString();*/
        //    int output = repository.ResetPassword(Email, Token);
        //    if (output == 100)
        //    {
        //        return BadRequest(new
        //        {
        //            status = HttpStatusCode.BadRequest,
        //            message = "Wrong Token !",
        //            /*error = e*/
        //        });
        //    }
        //    else if (output == 200)
        //    {
        //        return BadRequest(new
        //        {
        //            status = HttpStatusCode.BadRequest,
        //            message = "Wrong Email !",
        //            /*error = e*/
        //        });
        //    }
        //    return Ok(new
        //    {
        //        statusCode = StatusCode(200),
        //        status = HttpStatusCode.OK,
        //        message = "Password has been reset !"
        //    });
        //    /*return RedirectToAction()*/
        //}

        //return 100 for WrongEmail/not have Account
        //return 200 for WrongOldPassword
        //return 300 for same New and OldPassWord
        //return 400 for New and Confirm Password doesn't match 
        [HttpPost("change-password")]
        public ActionResult ChangePassword(ChangePasswordVM cpVM)
        {
            /*string tempEmail = Request.Query.Keys.Contains("email").ToString();*/
            int output = repository.ChangePassword(cpVM);
            if (output == 100)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "WrongEmail/don't have Account"
                    /*error = e*/
                });
            }
            else if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Old Password is Wrong"
                    /*error = e*/
                });
            }
            else if(output == 300)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "New and Old PassWord is same"
                    /*error = e*/
                });
            }

            else if (output == 400)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "New and Confirm Password doesn't match"
                    /*error = e*/
                });
            }
            return Ok(new
            {
                statusCode = StatusCode(200),
                status = HttpStatusCode.OK,
                message = "Password has been change !"
            });
            /*return RedirectToAction()*/
        }


    }
}
