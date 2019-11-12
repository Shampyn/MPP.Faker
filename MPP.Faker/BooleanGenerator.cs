using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Faker
{
    class BooleanGenerator : IGenerator<bool>
    {
        public bool Generate()
        {
            return true;
        }
    }
}
