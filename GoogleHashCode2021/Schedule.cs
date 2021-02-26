using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleHashCode2021
{
    public class Schedule
    {
        public int NrIncomingStreets { get; set; }
        public Dictionary<string, int> StreetLightSchedule { get; set; } = new Dictionary<string, int>();
    }
}
