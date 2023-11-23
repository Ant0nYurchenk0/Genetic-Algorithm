using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal struct Group
    {
        private static int _id;
        public int Id { get; private set; }
        public string Name { get; set; }
        public Group(string name)
        {
            Name = name;
            Id = _id++;
        }
    }
}
