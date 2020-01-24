using CarParkService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService.Classes
{
    public class ParkingSlot : IComparable<ParkingSlot>
    {

        public ParkingSlot(int slotNo)
        {
            Status = ParkingSlotType.Available;
            SlotNo = SlotNo;
        }

        public int SlotNo { get; private set; }

        public ParkingSlotType Status { get; set; }

        public Vehicle Vehicle { get; set; }

        public int CompareTo(ParkingSlot obj)
        {
            return this.Vehicle.RegistrationNo.CompareTo(obj.Vehicle.RegistrationNo);
        }

        public void Reset()
        {
            Status = ParkingSlotType.Available;
            Vehicle = null;
        }

        public void AssignedVehicle(Vehicle vehicle, ParkingSlotType status)
        {
            Vehicle = vehicle;
            Status = status;

        }
    }
}
