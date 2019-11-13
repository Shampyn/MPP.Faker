using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class CharGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return (char)random.Next(0, 256);
        }

        public Type GeneratedType()
        {
            return typeof(char);
        }
    }
}
