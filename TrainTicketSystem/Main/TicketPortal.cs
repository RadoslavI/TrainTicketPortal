using Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace TicketSystem
{
    public class TicketPortal
    {
        public static readonly TimeSpan SEVEN_THIRTY = new TimeSpan(7, 30, 0);
        public static readonly TimeSpan NINE_THIRTY = new TimeSpan(9, 30, 0);
        public static readonly TimeSpan SIXTEEN = new TimeSpan(16, 0, 0);
        public static readonly TimeSpan NINETEEN_THIRTY = new TimeSpan(19, 30, 0);

        private readonly ICollection<Train> trains;
        private readonly ICollection<Reservation> reservations;

        public TicketPortal()
        {
            trains = new List<Train>();
            reservations = new List<Reservation>();
        }

        public int TrainCount 
            => trains.Count;

        public void AddTrain(Train train)
        {
            trains.Add(train);
        }

        public void RemoveTrain(Train train)
        {
            trains.Remove(train);
        }

        public Train SearchTrain(string departure, string destination, string departureTime)
        {
            return trains.FirstOrDefault(t =>
            t.Departure == departure &&
            t.Destination == destination &&
            t.DepartureTime == departureTime);
        }

        public void CreateReservation(Train train, User user, TicketType ticket, int numberOfTickets)
        {
             if (train.AvailableSeats - numberOfTickets >= 0)
             {
                 double cost = calculateTicketPrice(train.DepartureTime, user.Cardtype, ticket, train.Price,
                     numberOfTickets, user.HasAChild);
                Console.WriteLine($"That would cost you {cost}$");
                Console.WriteLine($"{user.Name} have successfully reserved {numberOfTickets} seats on the selected train!");
                train.AvailableSeats -= numberOfTickets;

                var reservation = new Reservation(user, train, ticket);
                reservations.Add(reservation);
             }
             else
             {
                 Console.WriteLine("The selected train is full, please choose another one.");
             }
        }

        public List<Reservation> GetReservations(User user)
        => reservations.Where(r => r.User == user).ToList();

        public void DeleteReservation(Reservation reservation)
        {
            reservations.Remove(reservation);
        }

        public double calculateTicketPrice(string time, CardType card, TicketType ticket,
            double basePrice, int numberOfTickets, bool childIsUnderSixteen)
        {
            TimeSpan timeSpan = TimeSpan.Parse(time);

            if ((timeSpan > SEVEN_THIRTY && timeSpan < NINE_THIRTY) ||
                ((timeSpan > SIXTEEN) && (timeSpan < NINETEEN_THIRTY)))
            {
                Console.WriteLine("Rush Hour");
            }
            else
            {
                basePrice *= 0.95;
                Console.WriteLine("Regular");
            }

            double finalPrice;
            if (card == CardType.FAMILY)
            {
                if (childIsUnderSixteen)
                {
                    finalPrice = (numberOfTickets * basePrice) * 0.50;
                }
                else
                {
                    finalPrice = (numberOfTickets * basePrice) * 0.90;
                }
            }
            else if (card == CardType.RETIREMENT)
            {
                finalPrice = (basePrice * 0.66) + ((numberOfTickets - 1)  * basePrice);
            }
            else
            {
                finalPrice = basePrice * numberOfTickets;
            }

            if (ticket == TicketType.TWOWAY)
            {
                finalPrice *= 2;
            }

            return finalPrice;
        }
    }
}
