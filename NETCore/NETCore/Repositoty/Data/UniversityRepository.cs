using NETCore.Context;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class UniversityRepository: GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext): base (myContext)
        {

        }
    }
}
