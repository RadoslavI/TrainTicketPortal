namespace TicketSystem.Tests
{
    public class PortalTests
    {
        Train train;
        TicketPortal portal;

        [SetUp]
        public void Setup()
        {
            train = new Train();
            portal = new TicketPortal();
            train.Departure = "Sofia";
            train.Destination = "Plovdiv";
            train.DepartureTime = "8";
            train.AvailableSeats = 40;
            train.Price = 10;
            portal.AddTrain(train);
        }

        [Test]
        public void AddTrain_ShouldCreate_Train()
        {
            portal.AddTrain(new Train());
            Assert.That(portal.TrainCount == 2);
        }

        [Test]
        public void RemoveTrain_ShouldRemove_Train()
        {
            portal.RemoveTrain(train);
            Assert.That(portal.TrainCount == 0);
        }

        [Test]
        public void SearchTrain_Positive_ShouldReturn()
        {
            //Arrange
            //Act
            var searchTrain = portal.SearchTrain("Sofia", "Plovdiv", "8");

            //Assert
            Assert.That(train, Is.SameAs(searchTrain));
        }

        [Test]
        public void SearchTrain_Negative_ShouldNotReturn()
        {
            //Arrange
            //Act
            var searchTrain = portal.SearchTrain("Plovdiv", "Plovdiv", "8");

            //Assert
            Assert.That(train, Is.Not.SameAs(searchTrain));
        }

        //[Test]
        //public void Reserve_Positive_DecreasesSeats()
        //{
        //    //Arrange
        //    int result = 37;

        //    //Act
        //    portal.Reserve("Sofia", "Plovdiv", "8", 3, CardType.NONE, TicketType.TWOWAY, false);

        //    //Assert
        //    Assert.That(train.AvailableSeats == result);
        //}

        //[Test]
        //public void Reserve_Negative_DoesntDecreaseSeats()
        //{
        //    //Arrange
        //    int ticketsCount = 41;

        //    //Act
        //    portal.Reserve("Sofia", "Plovdiv", "8", ticketsCount, CardType.NONE, TicketType.TWOWAY, false);

        //    //Assert
        //    Assert.That(train.AvailableSeats == 40);
        //}

        [Test]
        public void calculateTicketPrice_Rush1way_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.NONE;
            var ticketType = TicketType.ONEWAY;
            string time = "8:00";
            double basePrice = 100;
            bool under16 = false;
            double expected = 100;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType, 
                basePrice, 1, under16);

            //Assert
            Assert.That(result == expected);
        }

        [Test]
        public void calculateTicketPrice_1way_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.NONE;
            var ticketType = TicketType.ONEWAY;
            string time = "6:00";
            double basePrice = 100;
            bool under16 = false;
            double expected = 95;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType,
                basePrice, 1, under16);

            //Assert
            Assert.That(result == expected);
        }

        [Test]
        public void calculateTicketPrice_NRushFamily1way_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.FAMILY;
            var ticketType = TicketType.ONEWAY;
            string time = "6:00";
            double basePrice = 100;
            bool under16 = false;
            double expected = 85.5;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType,
                basePrice, 1, under16);

            //Assert
            Assert.That(result == expected);
        }

        [Test]
        public void calculateTicketPrice_NRushFamily2way_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.FAMILY;
            var ticketType = TicketType.TWOWAY;
            string time = "6:00";
            double basePrice = 100;
            bool under16 = false;
            double expected = 171;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType,
                basePrice, 1, under16);

            //Assert
            Assert.That(result == expected);
        }

        [Test]
        public void calculateTicketPrice_RFamily2WayU16_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.FAMILY;
            var ticketType = TicketType.TWOWAY;
            string time = "8:00";
            double basePrice = 100;
            bool under16 = true;
            double expected = 100;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType,
                basePrice, 1, under16);

            //Assert
            Assert.That(result == expected);
        }

        [Test]
        public void calculateTicketPrice_RRetirement2Way_ReturnsResult()
        {
            //Arrange
            var cardType = CardType.RETIREMENT;
            var ticketType = TicketType.TWOWAY;
            string time = "8:00";
            double basePrice = 100;
            bool under16 = false;
            double expected = 532;

            //Act
            var result = portal.calculateTicketPrice(time, cardType, ticketType,
                basePrice, 3, under16);

            //Assert
            Assert.That(result == expected);
        }

    }
}