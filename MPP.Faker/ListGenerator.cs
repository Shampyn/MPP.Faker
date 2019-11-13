using IGeneratorLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class ListGenerator:IListGenerator
    {
        private Dictionary<Type, IGenerator> AvailableGenerators = new Dictionary<Type, IGenerator>();

        public ListGenerator(Dictionary<Type, IGenerator> Generators)
        {
            AvailableGenerators = Generators;
        }

        public object Generate(Type listtype)
        {
            IList listinstance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listtype));
            Random rnd = new Random();
            int listsize = rnd.Next(1, 10);
            for (int i = 0; i < listsize; i++)
            {
                IGenerator value;
                AvailableGenerators.TryGetValue(listtype, out value);
                if (value != null)
                {
                    listinstance.Add(value.Generate());
                }
                else
                {
                    listinstance.Add(null);
                }
            }
            return listinstance;
        }

    }
}
