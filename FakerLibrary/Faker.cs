using IGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
   public class Faker
    {

        private string _path = Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
        public Dictionary<Type, IGenerator> Generators = new Dictionary<Type, IGenerator>();
        public Stack<Type> generationStack = new Stack<Type>();
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
            listGenerator = new ListGenerator(Generators, this);
        }

        public object Create(Type type)
        {
            if (generationStack.Contains(type))
            {
             return null;
            }

            if (type.IsAbstract || type.IsInterface || type == typeof(void))
            {
              return null;
            }

            IGenerator value;
            Generators.TryGetValue(type, out value);

            if (value != null)
            {
                return value.Generate();
            }

            if (type.IsGenericType)
            {
                Type t = type.GetType();
                return listGenerator.Generate((Type)type.GenericTypeArguments.GetValue(0));
            }
                
                

                if (!type.IsAbstract || !type.IsPrimitive)
                {
                generationStack.Push(type);
                ConstructorInfo ConstructorWithMaxArgs = GetConstructorWithMaxParams(type);

                if (ConstructorWithMaxArgs != null)
                    {
                    var instance = GenerateObjectFromConstructor(ConstructorWithMaxArgs);
                    instance = GenerateFieldsAndProperties(type, instance);
                    generationStack.Pop();
                    return instance;
                    }
                    else
                    {
                    if (type.GetConstructors().Count() != 0)
                    {
                        var instance = Activator.CreateInstance(type);
                        instance = GenerateFieldsAndProperties(type, instance);
                        generationStack.Pop();
                        return instance;
                    }
                    else
                    {
                        return null;
                    }
                    }              
                }
                return null;
        }

        public T Create<T>()
        {
            if (Create(typeof(T)) == null)
            {
                return default(T);
            }
            return (T)Create(typeof(T));
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
                object value = Create(field.FieldType);
                field.SetValue(instance, value);
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    object value = Create(property.PropertyType);
                    property.SetValue(instance, value);
                }
            }
            return instance;
        }
    }
}


