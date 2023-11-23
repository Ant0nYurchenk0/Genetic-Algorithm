using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal struct Course
    {
        private static int _id = 0;

        public Course(string name, IEnumerable<Teacher> teachers)
        {
            Id = _id++;
            Name = name;
            Teachers = teachers;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
