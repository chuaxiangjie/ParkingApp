using ParkingApp.Service.Enums;
using ParkingApp.Service.Exceptions;
using ParkingApp.Service.Factories;
using ParkingApp.Service.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace ParkingApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            IClientCarParkService carParkService = CarParkServiceFactory.Build(CarParkModelType.SmartModel);

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

        static void ProcessFromFile(IClientCarParkService service, string fileName)
        {

            bool isExist = false;
            string filePath = "";

            //Check if file exists
            if (File.Exists(fileName))
            {
                isExist = true;
                filePath = fileName;
            }
            else
            {
                filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
                if (File.Exists(filePath))
                    isExist = true;
            }

            if (isExist)
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

        static void ProcessFromConsole(IClientCarParkService service)
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
