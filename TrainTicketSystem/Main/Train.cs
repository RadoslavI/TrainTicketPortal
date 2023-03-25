using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class Train
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public int AvailableSeats { get; set; }
        public double Price { get; set; }
    }
}
