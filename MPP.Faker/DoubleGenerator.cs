using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class DoubleGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return random.NextDouble();
        }

        public Type GeneratedType()
        {
            return typeof(double);
        }
    }
}
