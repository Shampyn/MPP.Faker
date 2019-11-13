using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    public interface IListGenerator
    {
        object Generate(Type type);
    }
}
