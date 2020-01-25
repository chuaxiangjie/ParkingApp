using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService.Models
{
    public abstract class Vehicle
    {
        public Vehicle(string registrationNo, string color)
        {

            RegistrationNo = registrationNo;
            Color = color;

        }

        //aka number plate
        public string RegistrationNo { get; set; }

        public string Color { get; set; }

    }
}
