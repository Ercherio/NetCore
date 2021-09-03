using NETCore.Context;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class RoleRepository : GeneralRepository<MyContext,Role,int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }

}
