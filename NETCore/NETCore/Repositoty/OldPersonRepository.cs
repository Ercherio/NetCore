using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Model;
using NETCore.Repositoty.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty
{
    public class OldPersonRepository : IPersonRepository
    {
        private readonly MyContext myContext;   //objek penampung

        public OldPersonRepository (MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            //throw new NotImplementedException();
            var wantDelete = myContext.Persons.Find(NIK);
            if (wantDelete.NIK == null)
            {
                throw new ArgumentNullException();
            }

            myContext.Persons.Remove(wantDelete);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public IEnumerable<Person> Get()
        {
            //throw new NotImplementedException();
            return myContext.Persons.ToList();

        }

        public Person Get(string NIK)
        {
            //throw new NotImplementedException();
            return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            //throw new NotImplementedException();
            //myContext.Persons.Add(person);
            //var insert = myContext.SaveChanges();
            ////try
            ////{
            ////    var insert = myContext.SaveChanges();
            ////}catch(Exception e)
            ////{
            //////    Console.WriteLine($"ERROR : {e.GetType()}");
            ////}
            //return insert;
            var insert = 0;
            myContext.Persons.Add(person);
            if (person.NIK != "")
            {
                insert = myContext.SaveChanges();

            }
            return insert;

        }

        public int Update(Person person)
        {
            //throw new NotImplementedException();
            //var cek= myContext.Persons.Find(NIK);
            //if(cek.NIK!= null)
            //{
                myContext.Entry(person).State=EntityState.Modified; //Where sudah diatur di sini
            //}
            var update = myContext.SaveChanges();
            return update;


        }
    }
}
