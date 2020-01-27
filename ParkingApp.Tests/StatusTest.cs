using NUnit.Framework;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Exceptions;
using ParkingApp.Service.Factories;
using ParkingApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class StatusTest
    {

        IClientCarParkService carParkService;

        [SetUp]
        public void Setup()
        {

            carParkService = CarParkServiceFactory.Build(CarParkModelType.SmartModel);

            string input = "create_parking_lot 10";
            carParkService.Execute(input);

            var parkInputs = TestHelper.Generate6ParkCarInputs();

            foreach (var parkInput in parkInputs)
            {
                carParkService.Execute(parkInput.Key);
            }
        }

        [TestCase("leave 4")]
        public void StatusCheck(string input)
        {

            carParkService.Execute(input);

            input = "status";

            var output = carParkService.Execute(input);

            List<string> list = new List<string>(
                           output.Split(new string[] { Environment.NewLine },
                           StringSplitOptions.RemoveEmptyEntries));

            //expecting total 6 records (+ 1 header)

            Assert.IsTrue(list.Count == 6);

            StringAssert.Contains("KA-01-HH-1234", list[1]);
            StringAssert.Contains("KA-01-HH-9999", list[2]);
            StringAssert.Contains("KA-01-BB-0001", list[3]);
            StringAssert.Contains("KA-01-HH-2701", list[4]);
            StringAssert.Contains("KA-01-HH-3141", list[5]);

            Assert.IsFalse(list.Contains("KA-01-HH-7777"));
        }


        [TestCase("status 12")]
        public void ErrorIfStatusCheckHasInvalidNumberOfParameters(string input)
        {
            Assert.Throws<ParkingSystemException>(() => carParkService.Execute(input));
        }

    }
}