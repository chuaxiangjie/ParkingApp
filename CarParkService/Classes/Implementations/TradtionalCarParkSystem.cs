using ParkingApp.Service.Interfaces;
using ParkingApp.Service.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Classes.Implementations
{
    public class TradtionalCarParkSystem : ICarParkSystem
    {
        Response ICarParkSystem.RegisterVehicleArrival(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        Response ICarParkSystem.RegisterVehicleExit(int slotNo)
        {
            throw new NotImplementedException();
        }
    }
}
