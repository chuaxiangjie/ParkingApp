using CarParkService.Classes;
using CarParkService.Exceptions;
using System;
using System.IO;
using System.Reflection;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var carParkService = CarParkService.Classes.CarParkService.Instance;
            int numberOfArguments = args.Length;

            if (numberOfArguments > 0)
            {
                // Execute commands via file
                ProcessFromFile(carParkService, args[0]);
            }
            else
            {
                // Execute commands via interactive console
                ProcessFromConsole(carParkService);
            }
        }

        static void ProcessFromFile(CarParkService.Classes.CarParkService service, string fileName)
        {
   
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);

            if (File.Exists(filePath))
            {

                string[] lines = File.ReadAllLines(filePath);

                try
                {
                    foreach (string input in lines)
                    {
                        var output = service.Execute(input);
                        Console.WriteLine(output);
                    }
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
            else
            {
                Console.WriteLine($"Unable to locate file path: {filePath}");
                Console.WriteLine("Exiting program..");
            }

        }

        static void ProcessFromConsole(CarParkService.Classes.CarParkService service)
        {

            while (true)
            {

                try
                {

                    string input = Console.ReadLine();

                    if (input.Equals("exit"))
                        break;

                    var output = service.Execute(input);
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
