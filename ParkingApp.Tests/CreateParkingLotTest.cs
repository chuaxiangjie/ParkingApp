using CarParkService.Exceptions;
using NUnit.Framework;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class CreateParkingLotTest
    {

        CarParkService.Classes.CarParkService carParkService;

        [SetUp]
        public void Setup()
        {
            carParkService = CarParkService.Classes.CarParkService.Instance;
        }


        [TestCase(1)]
        [TestCase(6)]
        [TestCase(1000)]
        public void CreateParkingLot(int numSlots)
        {

            string input = $"create_parking_lot {numSlots}";

            var output = carParkService.Execute(input);

            Assert.AreEqual(output, $"Created a parking lot with {numSlots} slots");
        }

        [TestCase("create_parking_lot P")]
        public void ErrorIfCreateParkingLotIsNotInteger(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

        [TestCase("create_parking_lot -1")]
        public void ErrorIfCreateParkingLotIsNegative(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

        [TestCase("create_parking_lot")]
        [TestCase("create_parking_lot 1 3")]
        public void ErrorIfCreateParkingLotInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

    }
}