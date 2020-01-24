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


            Console.WriteLine("Hello World!");
        }
    }
}
