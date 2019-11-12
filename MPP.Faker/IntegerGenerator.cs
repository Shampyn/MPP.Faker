using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class IntegerGenerator : IGenerator<int>
    {
        public int Generate()
        {
            Random random = new Random();

            if (random.Next(2) == 0)
            {
                return random.Next(1, int.MaxValue);
            }
            else
            {
                return random.Next(int.MinValue, -1);
            }
        }
    }
}
