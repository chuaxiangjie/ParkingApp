using ParkingApp.Service.Enums;
using ParkingApp.Service.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Models
{
    public class Car : Vehicle
    {

        public Car(string registrationNo, string color) : base(registrationNo, color, VehicleType.Car)
        {

        }

    }
}
