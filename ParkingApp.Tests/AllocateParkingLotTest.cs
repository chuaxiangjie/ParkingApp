using CarParkService.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class AllocateParkingLotTest
    {

        CarParkService.Classes.CarParkService carParkService;

        [SetUp]
        public void Setup()
        {
            carParkService = CarParkService.Classes.CarParkService.Instance;

            string input = "create_parking_lot 6";
            carParkService.Execute(input);

        }

        [Test]
        public void ParkOneCar()
        {

            string input = "park KA-01-HH-1234 White";

            var output = carParkService.Execute(input);

            Assert.AreEqual(output, "Allocated slot number: 1");
        }

        [Test]
        public void ParkMultipleCars()
        {

            var inputs = CommonHelper.Generate6ParkCarInputs();

            foreach (var input in inputs)
            {
                var output = carParkService.Execute(input.Key);

                Assert.AreEqual(output, input.Value);
            }
        }

        [Test]
        public void ParkMultipleCarsExceededTotalParkingLot()
        {

            var inputs = CommonHelper.Generate6ParkCarInputs();

            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-1111 Grey", "Sorry, parking lot is full"));
        
            foreach (var input in inputs)
            {
                var output = carParkService.Execute(input.Key);

                Assert.AreEqual(output, input.Value);
            }
        }

        [Test]
        public void ErrorIfParkCarInputHasInvalidNumberOfParameters()
        {

            string input = "park KA-01-HH-1234";

            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

    }
}