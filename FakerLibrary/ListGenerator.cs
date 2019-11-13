using IGeneratorLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary
{
    public  class ListGenerator : IListGenerator
    {
        private Dictionary<Type, IGenerator> _availableGenerators = new Dictionary<Type, IGenerator>();
        private Faker _currentFaker;
        private Random _random = new Random();

        public ListGenerator(Dictionary<Type, IGenerator> Generators, Faker MainFaker)
        {
            _availableGenerators = Generators;
            _currentFaker = MainFaker;
        }


        public object Generate(Type type)
        {
            IList listinstance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            int listsize = _random.Next(1, 21);
            for (int i = 0; i < listsize; i++)
            {
                listinstance.Add(_currentFaker.Create(type));
            }
            return listinstance;

        }
    }
}
