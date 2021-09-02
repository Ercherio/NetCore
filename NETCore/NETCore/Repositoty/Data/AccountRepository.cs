using NETCore.Context;
using NETCore.Model;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class AccountRepository:GeneralRepository<MyContext,Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext): base(myContext)
        {
            this.myContext = myContext;
        }

        public int ForgetPassword(string email)
        {
            /*Guid myuuid = Guid.NewGuid().ToString();
            string newPassword = myuuid.ToString();*/
            //100 = email ga ketemu
            var checkEmail = myContext.Persons.Where(e => e.Email == email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            /*account.Password = Guid.NewGuid().ToString();*/
            checkEmail.Token = Guid.NewGuid().ToString();
            string bodyEmail = $"Kamu lupa password ? Kalau iya, klik di sini https://localhost:44377/api/Accounts/reset-password/email={checkEmail.Email}&token={checkEmail.Token}, else abaikan";
            Email(bodyEmail, checkEmail.Email);
            myContext.SaveChanges();
            return 1;
        }

        public int ResetPassword(string email, string Token)
        {
            //return 100 = NIK salah
            //return 200 = email salah
            var checkEmail = myContext.Persons.Where(e => e.Email == email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 200;
            }
            if (checkEmail.Token != Token)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }

            string newPassword = Guid.NewGuid().ToString();
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            /*myContext.SaveChanges();*/
            Update(account);

            checkEmail.Token = null;
            myContext.SaveChanges();
            //kirim email
            string bodyEmail = $"Password baru Anda: {newPassword}, Jangan sebarkan dan segera lakukan change password";
            Email(bodyEmail, checkEmail.Email);
            return 1;
        }

        public int ChangePassword(ChangePasswordVM cpVM)
        {
            //return 100 for WrongEmail/not have Account
            //return 200 for WrongOldPassword
            //return 300 for same New and OldPassWord
            //return 400 for New and Confirm Password doesn't match 
            var checkEmail = myContext.Persons.Where(e => e.Email == cpVM.Email).FirstOrDefault();
            if(checkEmail == null)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();

            if (!BCrypt.Net.BCrypt.Verify(cpVM.OldPassword, account.Password))
            {
                return 200;
            }

            if (cpVM.NewPassword == cpVM.OldPassword)
            {
                return 300;
            }

            if(cpVM.NewPassword != cpVM.ConfirmPassword)
            {
                return 400;
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(cpVM.NewPassword);
            Update(account);

            return 1;
        }
    }
}
