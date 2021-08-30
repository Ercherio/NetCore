using NETCore.Context;
using NETCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repositoty.Data
{
    public class ProfilingRepository: GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext): base (myContext)
        {

        }
    }
}
