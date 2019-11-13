using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IGeneratorLibrary;

namespace FakerLibrary
{
    public class Plugin
    {
        public static readonly string path = "\\Plugins\\";

        public static List<Assembly> LoadPlugin(params string[] paths)
        {
            List<Assembly> Assemblies = new List<Assembly>();
            foreach (var file in paths)
                Assemblies.Add(Assembly.LoadFrom(file));
            return Assemblies;
        }

        public static Dictionary<Type, IGenerator> GetGenerators(List<Assembly> Assemblies)
        {
            Dictionary<Type, IGenerator> Generators = new Dictionary<Type, IGenerator>();
            foreach (Assembly Assembly in Assemblies)
            {
                try
                {
                    Type[] Types = Assembly.GetTypes();
                    Plugin.LoadGenerators(Generators, Types);
                }
                catch (Exception exception)
                {

                }
            }
            return Generators;
        }


        private static void LoadGenerators(Dictionary<Type, IGenerator> Generators, Type[] Types)
        {
            foreach (Type type in Types)
            {
                if (!type.IsInterface && !type.IsAbstract && typeof(IGenerator).IsAssignableFrom(type))
                {
                    try
                    {
                        IGenerator generator = Activator.CreateInstance(type) as IGenerator;
                        Generators.Add(generator.GeneratedType(), generator);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}
