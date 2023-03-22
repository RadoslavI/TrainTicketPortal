using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Main
{
    public class TicketPortal
    {
        public static TimeSpan SEVEN_THIRTY = new TimeSpan(7, 30, 0);
        public static TimeSpan NINE_THIRTY = new TimeSpan(9, 30, 0);
        public static TimeSpan SIXTEEN = new TimeSpan(16, 0, 0);
        public static TimeSpan NINETEEN_THIRTY = new TimeSpan(19, 30, 0);

        private readonly ICollection<Train> trains;

        public TicketPortal()
        {
            trains = new List<Train>();
        }
        public void AddTrain(Train train)
        {
            trains.Add(train);
        }

        public void RemoveTrain(Train train)
        {
            trains.Remove(train);
        }

        public Train SearchTrain(string departure, string destination, DateTime departureTime)
        {
            return trains.Where(t =>
            t.Departure == departure &&
            t.Destination == destination &&
            t.DepartureTime.Date == departureTime.Date &&
            t.DepartureTime.TimeOfDay == departureTime.TimeOfDay)
            .FirstOrDefault();
        }

        public void Reserve(string departure, string destination, DateTime departureTime, int numberOfTickets)
        {
            var searchedTrain = SearchTrain(departure, destination, departureTime);
            if (searchedTrain != null) 
            {
                if (searchedTrain.AvailableSeats - numberOfTickets >= 0)
                {
                    Console.WriteLine("Successfuly reserved a seat on the selected train!");
                    searchedTrain.AvailableSeats -= numberOfTickets;
                }
                else
                {
                    Console.WriteLine("The selected train is full, please choose another one.");
                }
            }
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
                finalPrice = (basePrice * 0.66) + (--numberOfTickets * basePrice);
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
