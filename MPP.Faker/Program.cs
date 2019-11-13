using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    public class A
    {
        int c;
        bool k;
        List<byte> T;
        public int testint { get; set; }
    }

    public class B
    {
        public C c { get; set; }
    }

    public class C
    {
        public B b { get; set; }
    }

    public class D
    {
        public  int a;
        public int b;
        public char k;
        public string Name { get; set; }
        public  bool c;
        public List<bool> BoolList;
        public D()
        {
            a = 10;
            b = 2;
        }
        public D(int a, int b, bool c, List<bool> listbool)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            BoolList = listbool;
        }
        public D(int k, int e)
        {
            a = e;
            b = k;
        }
    }

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

            D classtest = faker.Create<D>();

            Console.WriteLine("{0}, {1},{2}", classtest.k, classtest.a, classtest.b);
            Console.ReadLine();

        }
    }
}
