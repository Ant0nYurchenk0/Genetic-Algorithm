using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal struct Schedule
    {
        public IEnumerable<Class> Classes  {get; set; }
        public Schedule(IEnumerable<Class> classes)
        {
            Classes = classes;
        }

    }
}
