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
        public ListGenerator listGenerator;

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
            listGenerator = new ListGenerator(Generators);
        }

        public T Create<T>()
        {
            Type type = typeof(T);
            if (type.IsAbstract || type.IsInterface || type == typeof(void))
            {
                return default(T);
            }
            if (type.IsGenericType)
            {
                return (T)listGenerator.Generate((Type)type.GenericTypeArguments.GetValue(0));
            }
            IGenerator value;
            Generators.TryGetValue(type, out value);
            if (value != null)
            {
                return (T)value.Generate();
            }
            else
            {
                return default(T);
            }
        }


    }
}

