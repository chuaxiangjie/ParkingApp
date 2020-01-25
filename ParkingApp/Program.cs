using CarParkService.Classes;
using CarParkService.Exceptions;
using System;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var carParkService = CarParkService.Classes.CarParkService.Instance;

            while (true)
            {

                try
                {

                    string input = Console.ReadLine();

                    if (input.Equals("exit"))
                        break;

                    var output = carParkService.Execute(input);
                    Console.WriteLine(output);
                }
                catch (ParkingSystemException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
      
        }
    }
}
