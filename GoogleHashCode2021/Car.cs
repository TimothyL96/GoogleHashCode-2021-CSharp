using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleHashCode2021
{
    public class Car
    {
        public int ID { get; set; }
        public List<int> Paths { get; set; }
        public int Steps { get; set; } = 0;
    }
}
