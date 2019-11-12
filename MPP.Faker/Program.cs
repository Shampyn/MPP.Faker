using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class Program
    {
        static void Main(string[] args)
        {

            Faker faker = new Faker();
            int t = faker.Create<int>();
            bool b = faker.Create<bool>();
            Console.WriteLine("{0}, {1}", t, b);
            Console.ReadLine();
        }
    }
}
