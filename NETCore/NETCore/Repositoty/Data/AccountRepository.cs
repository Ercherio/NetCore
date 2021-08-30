using NETCore.Context;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class AccountRepository:GeneralRepository<MyContext,Account, string>
    {
        public AccountRepository(MyContext myContext): base(myContext)
        {

        }
    }
}
