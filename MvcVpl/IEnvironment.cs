using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcVpl
{
    public interface IEnvironment
    {
        string Execute(string source);
    }
}
