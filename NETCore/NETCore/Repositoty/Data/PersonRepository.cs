using NETCore.Context;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class PersonRepository:GeneralRepository<MyContext,Person,string>
    {
        public PersonRepository(MyContext myContext) : base(myContext)
        {

        }

    }
}
