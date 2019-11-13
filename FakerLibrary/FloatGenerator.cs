using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public class FloatGenerator : IGenerator
    {
        private Random _random = new Random();
        public object Generate()
        {          
            int sign = _random.Next(2);
            int exponent = _random.Next((1 << 8) - 1);
            int mantissa = _random.Next(1 << 23);
            float bits = (sign << 31) + (exponent << 23) + mantissa;
            return bits;
        }

        public Type GeneratedType()
        {
            return typeof(float);
        }
    }
}
