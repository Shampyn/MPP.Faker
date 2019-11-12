using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class ByteGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return (byte)random.Next(1, byte.MaxValue);
        }

        public Type GeneratedType()
        {
            return typeof(byte);
        }
    }
}
