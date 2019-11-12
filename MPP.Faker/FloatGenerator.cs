using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class FloatGenerator : IGenerator
    {
        public object Generate()
        {
            Random rnd = new Random();
            int sign = rnd.Next(2);
            int exponent = rnd.Next((1 << 8) - 1);
            int mantissa = rnd.Next(1 << 23);
            float bits = (sign << 31) + (exponent << 23) + mantissa;
            return bits;
        }

        public Type GeneratedType()
        {
            return typeof(float);
        }
    }
}
