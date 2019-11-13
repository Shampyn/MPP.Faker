using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public interface IListGenerator
    {
        object Generate(Type type);
    }
}
