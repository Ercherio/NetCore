﻿using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Model;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NETCore.Model.Person;
namespace NETCore.Repositoty.Data
{
    public class PersonRepository:GeneralRepository<MyContext,Person,string>
    {
        private readonly MyContext myContext;
        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<PersonVM> GetPersonVMs()
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                //join ar in myContext.AccountRoles on
                                //a.NIK equals ar.AccountId
                                //join r in myContext.Roles on
                                //ar.RoleId equals r.Id
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.Education_Id equals e.Id
                                join u in myContext.Universities on
                                e.UniversityId equals u.Id
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.FirstName + " " + p.LastName,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA,
                                    UniversityId = u.Id,
                                    AccountRoles = a.AccountRoles
                                }).ToList();
            return getPersonVMs;
        }

        public PersonVM GetPersonVMs(string NIK)
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                //join ar in myContext.AccountRoles on
                                //a.NIK equals ar.AccountId
                                //join r in myContext.Roles on
                                //ar.RoleId equals r.Id
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.Education_Id equals e.Id
                                join u in myContext.Universities on
                                e.UniversityId equals u.Id
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.FirstName + " " + p.LastName,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    Gender = p.gender,
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA,
                                    UniversityId = u.Id,
                                    //RoleId=ar.RoleId,
                                    AccountRoles = a.AccountRoles
                                }).Where(p => p.NIK == NIK).FirstOrDefault();
            return getPersonVMs;
        }
       

        public int Insert(PersonVM personVM)
        {
            //int insert;
            try
            {
                /*myContext.per.Add(entity);*/
                //Person person = new Person(personVM.NIK,
                //                               personVM.FirstName,
                //                               personVM.LastName,
                //                               personVM.Phone,
                //                               personVM.BirthDate,
                //                               personVM.Salary,
                //                               personVM.Email
                //                               //personVM.gender
                //                               );

                Person person = new Person();

                //input data ke objek
                person.NIK = personVM.NIK;
                if (isDuplicate(personVM.NIK.ToString(), "NIK") == true)
                {
                    return 200;
                }
                person.FirstName = personVM.FirstName;
                person.LastName = personVM.LastName;
                person.Email = personVM.Email;
                if (isDuplicate(personVM.Email.ToString(), "Email") == true) // cek Email Duplicate
                {
                    return 100;
                }

                person.Phone = personVM.Phone;
                if (isDuplicate(personVM.Phone.ToString(), "Phone") == true)
                {
                    return 300;
                }
                person.BirthDate = personVM.BirthDate;
                person.Salary = personVM.Salary;
                person.gender = (Gender)personVM.Gender;
                //if (myContext.IsValid)
                //{

                //}               
                myContext.Persons.Add(person);
                myContext.SaveChanges();



                Account account = new Account(personVM.NIK, 
                    BCrypt.Net.BCrypt.HashPassword(personVM.Password));
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                //if (personVM.RoleId == 0)
                //{
                //    personVM.RoleId = 1;

                //}


                AccountRole accountRole = new AccountRole(personVM.NIK, personVM.RoleId);
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();
                /*University university = new University("Temp Name");
                myContext.Universities.Add(university);*/
                //if (personVM.UniversityId == 0)
                //{
                //    insert = 0;
                //}

                Education education = new Education(personVM.Degree, personVM.GPA, personVM.UniversityId);
                myContext.Educations.Add(education);
                myContext.SaveChanges();

                Profiling profiling = new Profiling(personVM.NIK, education.Id);
                myContext.Profilings.Add(profiling);
                var insert = myContext.SaveChanges();

                return insert;
            }
            catch
            {
                throw new DbUpdateException();
            }
        }

        public bool isDuplicate(string word, string type)
        {
            var sql = $"SELECT * From tb_m_persons Where {type} = '{word}'";
            var data = myContext.Persons.FromSqlRaw(sql).FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public int Login(LoginVM loginVM)
        {
            //return 100 = email ga ketemu
            //return 200 = password salah
            //return 1 = login berhasil
            var checkEmail = myContext.Persons.Where(e => e.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 100;
            }
            /*if(checkEmail)*/
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            //if (account.Password != loginVM.Password)
            if(!BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
            {
                return 200;
            }


            return 1;
        }

        //public RoleVM GetRole(string nIK, int roleId)
        //{
        //    if (nIK == null && roleId < 0)
        //    {
        //        return null;
        //    }

        //    if(myContext.AccountRoles.Where(a => a.AccountId == nIK && a.RoleId == roleId).Count()< 0)
        //    {
        //        return null;
        //    }

        //    return (from acc in myContext.Accounts
        //            join accrole in myContext.AccountRoles on
        //            acc.NIK equals accrole.AccountId
        //            join role in myContext.Roles on
        //            accrole.RoleId equals role.Id
        //            select new RoleVM
        //            {
        //                NIK = acc.NIK,
        //                RoleId = accrole.RoleId,
        //                RoleName = role.Name

        //            }).Where(acc => acc.NIK == nIK && acc.RoleId == roleId).FirstOrDefault();
        //}


    }
}
