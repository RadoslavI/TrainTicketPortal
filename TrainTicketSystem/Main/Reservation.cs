using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem;

namespace Main
{
    public class Reservation
    {
        public Reservation(User user, Train train, TicketType ticketType)
        {
            User = user;
            Train = train;
            TicketType = ticketType;
        }

        public User User { get; set; }
        public Train Train { get; set; }
        public TicketType TicketType { get; set; }
    }
}
