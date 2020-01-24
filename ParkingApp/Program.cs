using CarParkService.Classes;
using System;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = "create_parking_lot 6";

            var carParkService = CarParkService.Classes.CarParkService.Instance;

            var output = carParkService.Execute(input);

            Console.WriteLine("Hello World!");
        }
    }
}
