using ParkingApp.Service.Classes;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Interfaces
{

    //Government Regulation to search information
    public interface ISmartCarParkPlugin : ICarParkSystem
    {

        Response GetCarSlotsStatus(ParkingSlotType slotStatus);

        IList<ParkingSlot> GetOccupiedSlotsByColour(string colour);

        IList<ParkingSlot> GetOccupiedSlotsByRegistrationNumber(string registrationNumber);

    }
}
