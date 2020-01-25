using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Tests
{
    public static class TestHelper
    {
        public static List<KeyValuePair<string, string>> Generate6ParkCarInputs()
        {
            List<KeyValuePair<string, string>> inputs = new List<KeyValuePair<string, string>>();

            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-1234 White", "Allocated slot number: 1"));
            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-9999 White", "Allocated slot number: 2"));
            inputs.Add(new KeyValuePair<string, string>("park KA-01-BB-0001 Black", "Allocated slot number: 3"));
            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-7777 Red", "Allocated slot number: 4"));
            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-2701 Blue", "Allocated slot number: 5"));
            inputs.Add(new KeyValuePair<string, string>("park KA-01-HH-3141 Black", "Allocated slot number: 6"));

            return inputs;
        }


    }
}
