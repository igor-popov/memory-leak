using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicAssembly.Server
{
    interface IExternalMicroservice
    {
        string[] Values();
    }
}
