using CarParkService.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class LeaveParkingLotTest
    {

        CarParkService.Classes.CarParkService carParkService;

        [SetUp]
        public void Setup()
        {
            carParkService = CarParkService.Classes.CarParkService.Instance;

            string input = "create_parking_lot 6";
            carParkService.Execute(input);

            input = "park KA-01-HH-1234 White";
            carParkService.Execute(input);

        }

        [TestCase(1)]
        [TestCase(5)]
        public void LeaveParkingLot(int numSlot)
        {

            string input = $"leave {numSlot}";

            var output = carParkService.Execute(input);

            Assert.AreEqual(output, $"Slot number {numSlot} is free");
        }

        [TestCase("leave")]
        [TestCase("leave a")]
        public void ErrorIfParkCarInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

    }
}