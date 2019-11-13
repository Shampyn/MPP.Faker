using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class Faker
    {

        private string _path = Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
        public Dictionary<Type, IGenerator> Generators = new Dictionary<Type, IGenerator>();
        public Stack<Type> generationStack;

        public Faker()
        {
            
            List<Assembly> Plugins = Plugin.LoadPlugin(_path + Plugin.path + "IntegerGenerator.dll",_path + Plugin.path + "StringGenerator.dll");
         

            Generators = Plugin.GetGenerators(Plugins);
            Generators.Add(typeof(bool), new BooleanGenerator());
            Generators.Add(typeof(byte), new ByteGenerator());
            Generators.Add(typeof(long), new LongGenerator());
            Generators.Add(typeof(float), new FloatGenerator());
            Generators.Add(typeof(double), new DoubleGenerator());
            Generators.Add(typeof(char), new CharGenerator());
        }

        public T Create<T>()
        {
            foreach (KeyValuePair<Type, IGenerator> generator in Generators)
            {
                if (generator.Key == typeof(T))
                    return (T)generator.Value.Generate();
            }
            return default(T);
                     
        }


    }
}

