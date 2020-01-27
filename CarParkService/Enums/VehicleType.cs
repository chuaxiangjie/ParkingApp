using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Enums
{

    /// <summary>
    /// Represents a vehicle type
    /// </summary>
    [Flags]
    public enum VehicleType
    {
       Car,
       Van,
       MotorCycle,
       Lorry
    }

}
