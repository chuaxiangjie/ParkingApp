using CarParkService.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class SearchTest
    {

        CarParkService.Classes.CarParkService carParkService;

        [SetUp]
        public void Setup()
        {
            carParkService = CarParkService.Classes.CarParkService.Instance;

            string input = "create_parking_lot 10";
            carParkService.Execute(input);

            var parkInputs = CommonHelper.Generate6ParkCarInputs();

            foreach (var parkInput in parkInputs)
            {
                carParkService.Execute(parkInput.Key);
            }

        }

        [TestCase("registration_numbers_for_cars_with_colour Black")]
        public void Can_search_registration_numbers_by_color(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            string[] results = output.Split(",");

            //expecting total 2 records

            Assert.IsTrue(results.Length == 2);

            Assert.AreEqual("KA-01-BB-0001", results[0]);
            Assert.AreEqual("KA-01-HH-3141", results[1]);

        }


        [TestCase("registration_numbers_for_cars_with_colour")]
        [TestCase("registration_numbers_for_cars_with_colour Whit e ")]
        public void ErrorIfRegistrationByColorInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

    }
}