using ParkingApp.Service.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Service.Models
{
    public abstract class Vehicle
    {
        public Vehicle(string registrationNo, string color, VehicleType type)
        {

            RegistrationNo = registrationNo;
            Color = color;
            Type = type;

        }

        //aka number plate
        public string RegistrationNo { get; set; }

        public string Color { get; set; }

        public VehicleType Type { get; set; }

    }
}
