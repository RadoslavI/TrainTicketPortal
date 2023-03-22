using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Train
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public int AvailableSeats { get; set; }
        public double Price { get; set; }
        public bool IsAvailable 
            => AvailableSeats > 0;
    }
}
