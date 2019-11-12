using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class StringGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            int length = random.Next(1, 10); 
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += (char)random.Next(0, 256);
            }
            return result;
        }

        public Type GeneratedType()
        {
            return typeof(string);
        }
    }
}
