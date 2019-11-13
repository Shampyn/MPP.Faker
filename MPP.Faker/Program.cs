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
            byte c = faker.Create<byte>();        
            float f = faker.Create<float>();        
            char ch = faker.Create<char>();
            double d = faker.Create<double>();
            long lng = faker.Create<long>();
            string str = faker.Create<string>();
            List<byte> listtest = faker.Create<List<byte>>();
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", t, b, c, f, ch, d, lng, str);
            Console.WriteLine();
            for (int i = 0; i < listtest.Count(); i++)
            {
                Console.WriteLine("{0}", listtest.ElementAt(i));
            }
            Console.ReadLine();

        }
    }
}
