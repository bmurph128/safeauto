using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeAutoDriverUpload.Data
{
    public class Trip
    {
        public string Command { get; set; }
        public string Name { get; set; }
        public DateTime TripStartTime { get; set; }
        public DateTime TripEndTime { get; set; }
        public double MilesDriven { get; set; }

    }
}
