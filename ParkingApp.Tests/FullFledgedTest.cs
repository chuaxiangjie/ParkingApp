using CarParkService.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ParkingApp.Tests
{

    [TestFixture]
    public class FullFledgedTest
    {

        string[] commands;
        string[] source_results;

        [SetUp]
        public void Setup()
        {

            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"File{Path.DirectorySeparatorChar}file_input.txt");

            if (File.Exists(filePath))
            {
                commands = File.ReadAllLines(filePath);
            }
            else
            {
                Console.WriteLine($"Unable to locate file path: {filePath}");
            }

            filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"File{Path.DirectorySeparatorChar}file_output.txt");

            if (File.Exists(filePath))
            {
                source_results = File.ReadAllLines(filePath);
            }
            else
            {
                Console.WriteLine($"Unable to locate file path: {filePath}");
            }
        }

        [Test]
        public void Compare_input_and_output_file_text()
        {

            StringBuilder sb = new StringBuilder();


            foreach (string command in commands)
            {
                var output = CarParkService.Classes.CarParkService.Instance.Execute(command);
                sb.AppendLine(output);
            }

            var finalOutput = sb.ToString().Trim();

            //compare final output with file
            string[] lines = finalOutput.Split(System.Environment.NewLine);

            Assert.AreEqual(lines.Length, source_results.Length);

            CollectionAssert.AreEqual(lines, source_results);

        }

    }
}