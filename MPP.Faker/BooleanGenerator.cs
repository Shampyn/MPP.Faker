using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class BooleanGenerator : IGenerator
    {
        public Type GeneratedType()
        {
            return typeof(bool);
        }

        public object Generate()
        {
            return true;   
        }
    }
}
