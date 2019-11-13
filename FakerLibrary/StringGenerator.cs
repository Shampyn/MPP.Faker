using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public class StringGenerator : IGenerator
    {
        private Random _random = new Random();
        public object Generate()
        {
            
            int length = _random.Next(1, 10); 
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += (char)_random.Next(0, 256);
            }
            return result;
        }

        public Type GeneratedType()
        {
            return typeof(string);
        }
    }
}
