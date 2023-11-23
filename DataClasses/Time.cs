﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal struct Time
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public string TimeStr { get; set; }
        public Time(string timeStr)
        {
            TimeStr = timeStr;
            Id = _id++;
        }
    }
}
