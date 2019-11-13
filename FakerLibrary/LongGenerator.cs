using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeneratorLibrary;

namespace FakerLibrary
{
    public class LongGenerator : IGenerator
    {
        private Random _random = new Random();
        public object Generate()
        {
            byte[] bytes = new byte[8];
            _random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public Type GeneratedType()
        {
            return typeof(long);
        }
    }
}
