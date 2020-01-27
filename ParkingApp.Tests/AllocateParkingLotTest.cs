using NUnit.Framework;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Exceptions;
using ParkingApp.Service.Factories;
using ParkingApp.Service.Interfaces;
using System.Collections.Generic;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class AllocateParkingLotTest
    {

        IClientCarParkService carParkService;

        [SetUp]
        public void Setup()
        {
            carParkService = CarParkServiceFactory.Build(CarParkModelType.SmartModel);

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

            var inputs = TestHelper.Generate6ParkCarInputs();

            foreach (var input in inputs)
            {
                var output = carParkService.Execute(input.Key);

                Assert.AreEqual(output, input.Value);
            }
        }

        [Test]
        public void ParkMultipleCarsExceededTotalParkingLot()
        {

            var inputs = TestHelper.Generate6ParkCarInputs();

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