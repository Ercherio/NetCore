using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Repositoty.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NETCore.Repositoty
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class 
        where Context : MyContext

    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> dbSet;

        public GeneralRepository (MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Entity>();
        }
        public int Delete(Key key)
        {
            //throw new NotImplementedException();
            var wantDelete = dbSet.Find(key);
            if (wantDelete == null)
            {
                throw new ArgumentNullException();
            }

            dbSet.Remove(wantDelete);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public IEnumerable<Entity> Get()
        {
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            return dbSet.Find(key);
        }

        public int Insert(Entity entity)
        {

            //var insert = 0;
            try
            {
                dbSet.Add(entity);
                var insert = myContext.SaveChanges();
                return insert;
            }
            catch
            {
                throw new DbUpdateException();
            }
        }

        public int Update(Entity entity)
        {
            //throw new NotImplementedException();
            myContext.Entry(entity).State = EntityState.Modified; //Where sudah diatur di sini
            //}
            var update = myContext.SaveChanges();
            return update;
        }

        public static void Email(string htmlString, string toMailAddress)
        {
            string fromMail = "ercheriom@gmail.com";
            string fromPassword = "Vongola_123";
            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toMailAddress));
            message.Subject = "Test";
            message.Body = "<html><body>" + htmlString + "<html><body>";
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,

            };

            smtpClient.Send(message);
        }
        //public static void Email(string htmlString, string toMailAddress)
        //{
        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        SmtpClient smtp = new SmtpClient();
        //        message.From = new MailAddress("ercheriom@gmail.com");
        //        message.To.Add(new MailAddress(toMailAddress));
        //        message.Subject = "Test";
        //        message.IsBodyHtml = true; //to make message body as html  
        //        message.Body = htmlString;
        //        smtp.Port = 587;
        //        smtp.Host = "smtp.gmail.com"; //for gmail host  
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Send(message);
        //    }
        //    catch (Exception) { }
        //}

        //public static void Email(string body, string toMailAddress)
        //{
        //    SmtpClient smtpClient = new SmtpClient("smtp.mailgun.org", 587);

        //    smtpClient.Credentials = new System.Net.NetworkCredential("postmaster@sandboxd1a3cc40065b464083b637c8c1c8f115.mailgun.org", "88150a33edbfa8e4aa72d10f5a5c62d5-c4d287b4-21523c1b");
        //    //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
        //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtpClient.EnableSsl = true;
        //    MailMessage mail = new MailMessage();

        //    //Setting From , To and CC
        //    mail.From = new MailAddress("postmaster@sandboxd1a3cc40065b464083b637c8c1c8f115.mailgun.org", "MyWeb Site");
        //    mail.To.Add(new MailAddress(toMailAddress));
        //    mail.Body = body;

        //    smtpClient.Send(mail);
        //}
    }
}
