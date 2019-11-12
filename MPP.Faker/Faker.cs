using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class Faker
    {
        public T Create<T>()
        {
            if (typeof(T) == typeof(int))
            {
                IntegerGenerator intgen = new IntegerGenerator();
                int test = intgen.Generate();
                return (T)(object)test;
            }
            if (typeof(T) == typeof(bool))
            {
                BooleanGenerator boolgen = new BooleanGenerator();
                bool test = boolgen.Generate();
                return (T)(object)test;
            }
            else
            {
                return default(T);
            }
            
        }


    }
}

