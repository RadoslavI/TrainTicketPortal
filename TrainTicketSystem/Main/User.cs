using System.Collections.Generic;
using TicketSystem;

namespace Main
{
    public class User
    {
        public string Name { get; set; }
        public CardType Cardtype { get; set; }
        public bool HasAChild { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
            = new List<Reservation>();
    }
}