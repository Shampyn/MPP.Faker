using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public class CharGenerator : IGenerator
    {
        private Random _random = new Random();

        public object Generate()
        {
          return (char)_random.Next(0, 256);
        }

        public Type GeneratedType()
        {
            return typeof(char);
        }
    }
}
