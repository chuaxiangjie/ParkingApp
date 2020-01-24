using CarParkService.Enums;
using CarParkService.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CarParkService.Classes
{
    public class CarPark
    {

        // <K,V> - <slotNo, ParkingSlot>
        Dictionary<int, ParkingSlot> _occupiedParkingSlots { get; set; }
       
        AvailableParkingSlots _availableParkingSlots { get; set; }

        public CarPark()
        {

            //Initialize ParkingSlots
            _occupiedParkingSlots = new Dictionary<int, ParkingSlot>();

            _availableParkingSlots = new AvailableParkingSlots();

        }

        public void CreateParkingSlots(int number)
        {

            if (number <= 0)
                return;

            for (int i = 1; i < number + 1; i++)
            {
                ParkingSlot newSlot = new ParkingSlot(i);

                _availableParkingSlots.Add(newSlot);
            }

            /*  Visualize as Min Heap (order from slot 1, 2, 3 to eg 6) */

        }

        public string AssignCarSlot(string registrationNo, string color)
        {

            //Check for available parking lots

            if (_availableParkingSlots.GetSize() > 0)
            {
                // assign an empty slot, pop heap
                var assignedSlot = _availableParkingSlots.PopMin();

                Vehicle targetVehicle = new Car(registrationNo, color);

                assignedSlot.AssignedVehicle(targetVehicle, ParkingSlotType.Reserved);

                _occupiedParkingSlots.Add(assignedSlot.SlotNo, assignedSlot);

                return $"Allocated slot number: {assignedSlot.SlotNo}";

            }
            else
            {
                // carpark is full
                return $"Sorry, parking lot is full";
            }
        }

        public string ReleaseCarSlot(int slotNo)
        {

            if (_occupiedParkingSlots.ContainsKey(slotNo))
            {
                var targetParkingSlot = _occupiedParkingSlots[slotNo];

                //remove slot from occupied slots
                _occupiedParkingSlots.Remove(slotNo);

                _availableParkingSlots.Add(targetParkingSlot);

                return $"Slot number {targetParkingSlot.SlotNo} is free";
            }
            else
            {
                // slotNo is not currently occupied
            }

            return "Error";

        }


        public int Count => _occupiedParkingSlots.Count();

        public bool IsCarParkFull() => _availableParkingSlots.GetSize() == 0;

    }
}
