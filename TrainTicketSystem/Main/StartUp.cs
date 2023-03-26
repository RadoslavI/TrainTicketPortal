using System;
namespace TicketSystem
{
    public static class StartUp
    {
        static void Main(string[] args)
        {
            string time;
            var portal = new TicketPortal();

            Console.Write("Enter time: ");
            time = Console.ReadLine();

            Console.WriteLine(portal.calculateTicketPrice(time, CardType.RETIREMENT, TicketType.ONEWAY, 100, 1, false));
            Console.WriteLine(portal.calculateTicketPrice(time, CardType.FAMILY, TicketType.ONEWAY, 100, 1, true));
            Console.WriteLine(portal.calculateTicketPrice(time, CardType.NONE, TicketType.ONEWAY, 100, 1, false));
            Console.WriteLine(portal.calculateTicketPrice(time, CardType.NONE, TicketType.TWOWAY, 100, 1, true));
        }
    }
}
