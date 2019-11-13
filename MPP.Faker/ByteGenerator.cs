using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class ByteGenerator : IGenerator
    {
        private Random _random = new Random();

        public object Generate()
        {
            
            return (byte)_random.Next(1, byte.MaxValue);
        }

        public Type GeneratedType()
        {
            return typeof(byte);
        }
    }
}
