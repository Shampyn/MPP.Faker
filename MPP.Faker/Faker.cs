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

                if (!type.IsAbstract || !type.IsPrimitive)
                {
                    ConstructorInfo ConstructorWithMaxArgs = GetConstructorWithMaxParams(type);
                    var instance = GenerateObjectFromConstructor(ConstructorWithMaxArgs);
                    return (T)GenerateFieldsAndProperties(type, instance);
                }
                return default(T);
            }

        private ConstructorInfo GetConstructorWithMaxParams(Type type)
        {
            ConstructorInfo constructorwithmaxparams = null;
            int count = 0;
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (count < constructor.GetParameters().Count())
                {
                    constructorwithmaxparams = constructor;
                    count = constructor.GetParameters().Count();
                }
            }
            return constructorwithmaxparams;
        }

        private object GenerateObjectFromConstructor(ConstructorInfo constructor)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parametersValues = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsGenericType)
                {
                    parametersValues[i] = listGenerator.Generate((Type)parameters[i].ParameterType.GenericTypeArguments.GetValue(0));
                }
                else
                {
                    IGenerator valueGenerator;
                    Generators.TryGetValue(parameters[i].ParameterType, out valueGenerator);
                    parametersValues[i] = valueGenerator.Generate();
                }
            }
            return constructor.Invoke(parametersValues);
        }

        private object GenerateFieldsAndProperties(Type type, object instance)
        {
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                IGenerator value;
                Generators.TryGetValue(field.FieldType, out value);
                field.SetValue(instance, value.Generate());
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsGenericType) 
                {
                    property.SetValue(instance, listGenerator.Generate((Type)property.PropertyType.GenericTypeArguments.GetValue(0)));                  
                }
                else
                {
                    IGenerator value;
                    Generators.TryGetValue(property.PropertyType, out value);
                    property.SetValue(instance, value.Generate());
                }               
            }
            return instance;
        }
    }
}


