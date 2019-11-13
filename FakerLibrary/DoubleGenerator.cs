using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public class DoubleGenerator : IGenerator
    {
        private Random _random = new Random();

        public object Generate()
        {
            
            return _random.NextDouble();
        }

        public Type GeneratedType()
        {
            return typeof(double);
        }
    }
}
