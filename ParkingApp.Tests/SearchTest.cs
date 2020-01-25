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

            var parkInputs = TestHelper.Generate6ParkCarInputs();

            foreach (var parkInput in parkInputs)
            {
                carParkService.Execute(parkInput.Key);
            }

        }

        #region Search : registration_numbers_for_cars_with_colour

        [TestCase("registration_numbers_for_cars_with_colour Black")]
        public void Can_search_registration_numbers_by_color(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            string[] results = output.Split(", ");

            //expecting total 2 records

            Assert.IsTrue(results.Length == 2);

            Assert.AreEqual("KA-01-BB-0001", results[0]);
            Assert.AreEqual("KA-01-HH-3141", results[1]);

        }

        [TestCase("registration_numbers_for_cars_with_colour Green")]
        [TestCase("registration_numbers_for_cars_with_colour Cyan")]
        public void Unable_to_search_registration_numbers_by_color(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            StringAssert.AreEqualIgnoringCase(output, "Not found");

        }

        [TestCase("registration_numbers_for_cars_with_colour")]
        [TestCase("registration_numbers_for_cars_with_colour Whit e ")]
        public void ErrorIfRegistrationByColorInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

        #endregion

        #region Search : slot_numbers_for_cars_with_colour

        [TestCase("slot_numbers_for_cars_with_colour Black")]
        public void Can_search_slot_numbers_for_cars_by_color(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            string[] results = output.Split(", ");

            //expecting total 2 records
            Assert.IsTrue(results.Length == 2);

            StringAssert.AreEqualIgnoringCase(results[0], "3");
            StringAssert.AreEqualIgnoringCase(results[1], "6");

        }

        [TestCase("slot_numbers_for_cars_with_colour Yellow")]
        [TestCase("slot_numbers_for_cars_with_colour Purple")]
        public void Unable_to_search_slot_numbers_for_cars_by_color(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            StringAssert.AreEqualIgnoringCase(output, "Not found");

        }


        [TestCase("slot_numbers_for_cars_with_colour")]
        [TestCase("slot_numbers_for_cars_with_colour Blac k")]
        public void ErrorIfSlotNumbersByColorInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

        #endregion

        #region Search : slot_number_for_registration_number

        [TestCase("slot_number_for_registration_number KA-01-HH-2701")]
        public void Can_search_slot_numbers_by_registration_number(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            string[] results = output.Split(", ");

            //expecting total 1 records
            Assert.IsTrue(results.Length == 1);

            StringAssert.AreEqualIgnoringCase(results[0], "5");

        }

        [TestCase("slot_number_for_registration_number MH-04-AY-1111")]
        [TestCase("slot_number_for_registration_number abc")]
        public void Unable_to_search_slot_numbers_by_registration_number(string input)
        {

            carParkService.Execute(input);

            var output = carParkService.Execute(input);

            StringAssert.AreEqualIgnoringCase(output, "Not found");

        }

        [TestCase("slot_number_for_registration_number")]
        [TestCase("slot_number_for_registration_number KA-01-HH- 2701")]
        public void ErrorIfSlotNumbersByRegistrationNumberInputHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

        #endregion

    }
}