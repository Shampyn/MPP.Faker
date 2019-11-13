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
            listGenerator = new ListGenerator(Generators);
        }
      
        public T Create<T>()
            {
                Type type = typeof(T);

                if (generationStack.Contains(type))
                {
                return default(T);
                }

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
                    generationStack.Push(type);
                    ConstructorInfo ConstructorWithMaxArgs = GetConstructorWithMaxParams(type);
                    if (ConstructorWithMaxArgs != null)
                    {
                    var instance = GenerateObjectFromConstructor(ConstructorWithMaxArgs);
                    instance = (T)GenerateFieldsAndProperties(type, instance);
                    generationStack.Pop();
                    return (T)instance;
                    }
                    else
                    {
                    if (type.GetConstructors().Count() != 0)
                    {
                        var instance = Activator.CreateInstance(type);
                        instance = (T)GenerateFieldsAndProperties(type, instance);
                        generationStack.Pop();
                        return (T)instance;
                    }
                    else
                    {
                        return default(T);
                    }
                    }              
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
                MethodInfo method = typeof(Faker).GetMethod("Create");
                MethodInfo genericMethod = method.MakeGenericMethod(field.FieldType);
                object value = genericMethod.Invoke(this, null);
                field.SetValue(instance, value);
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                MethodInfo method = typeof(Faker).GetMethod("Create");
                MethodInfo genericMethod = method.MakeGenericMethod(property.PropertyType);
                object value = genericMethod.Invoke(this, null);
                property.SetValue(instance, value);
            }
            return instance;
        }
    }
}


