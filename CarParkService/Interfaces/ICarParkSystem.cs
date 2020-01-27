using ParkingApp.Service.Classes;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Models;
using ParkingApp.Service.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Interfaces
{
    public interface ICarParkSystem
    {

        Response RegisterVehicleArrival(Vehicle vehicle);

        Response RegisterVehicleExit(int slotNo);

    }
}
