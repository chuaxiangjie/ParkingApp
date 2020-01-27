using ParkingApp.Service.Enums;
using ParkingApp.Service.Service.Models;
using System;
using System.Text;

namespace ParkingApp.Service.Models
{
    public class ParkingSlot : IComparable<ParkingSlot>
    {

        #region Fields

        public int SlotNo { get; private set; }

        public ParkingSlotType Status { get; set; }

        public Vehicle Vehicle { get; set; }

        #endregion

        #region Ctor
        public ParkingSlot(int slotNo)
        {
            Status = ParkingSlotType.Available;
            SlotNo = slotNo;
        }

        #endregion

        #region Methods
        public int CompareTo(ParkingSlot obj)
        {
            return this.SlotNo.CompareTo(obj.SlotNo);
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

        #endregion

    }
}
