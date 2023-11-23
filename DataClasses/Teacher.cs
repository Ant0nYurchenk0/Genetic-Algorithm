﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal struct Teacher
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public Teacher(string name)
        {
            Name = name;
            Id = _id++;
        }
    }
}
