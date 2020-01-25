using CarParkService.Enums;
using CarParkService.Exceptions;
using CarParkService.Models;
using Libraries.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkService.Classes
{
    internal class CarParkSystem
    {

        // <K,V> - <slotNo, ParkingSlot>
        Dictionary<int, ParkingSlot> _occupiedParkingSlots { get; set; }

        AvailableParkingSlots _availableParkingSlots { get; set; }

        public CarParkSystem()
        {

            //Initialize ParkingSlots
            _occupiedParkingSlots = new Dictionary<int, ParkingSlot>();

            _availableParkingSlots = new AvailableParkingSlots();

        }

        public void CreateParkingSlots(int number)
        {

            if (CheckIfParkingLotIsCreated())
                Destroy();

            for (int i = 1; i < number + 1; i++)
            {
                ParkingSlot newSlot = new ParkingSlot(i);

                _availableParkingSlots.Add(newSlot);
            }

            /*  Visualize as Min Heap (order from slot 1, 2, 3 to eg 6) */

        }

        public string AssignCarSlot(string registrationNo, string color)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            //Check if registrationNo exists in occupied parking slots
            var existingOccupiedSlot = _occupiedParkingSlots.SingleOrDefault(x => x.Value.Vehicle.RegistrationNo == registrationNo);

            if (!existingOccupiedSlot.Equals(default(KeyValuePair<int, ParkingSlot>)))
            {
                // clear existing occupied slot
                _occupiedParkingSlots.Remove(existingOccupiedSlot.Key);
                _availableParkingSlots.Add(existingOccupiedSlot.Value);
            }


            //Check for available parking lots

            if (_availableParkingSlots.GetSize() > 0)
            {
                // assign an empty slot, pop heap
                var assignedSlot = _availableParkingSlots.PopMin();

                Vehicle targetVehicle = new Car(registrationNo, color);

                assignedSlot.AssignedVehicle(targetVehicle, ParkingSlotType.Occupied);

                _occupiedParkingSlots.Add(assignedSlot.SlotNo, assignedSlot);

                return $"Allocated slot number: {assignedSlot.SlotNo}";

            }
            else
            {
                // carpark is full
                return $"Sorry, parking lot is full";
            }
        }

        public void ReleaseCarSlot(int slotNo)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            if (_occupiedParkingSlots.ContainsKey(slotNo))
            {
                var targetParkingSlot = _occupiedParkingSlots[slotNo];

                //remove slot from occupied slots
                _occupiedParkingSlots.Remove(slotNo);

                _availableParkingSlots.Add(targetParkingSlot);
            }
            else
            {
                // slotNo is not currently occupied
            }
        }

        public string GetCarSlotsStatus(ParkingSlotType slotStatus)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            StringBuilder strBuilder = new StringBuilder();
            IList<ParkingSlot> parkingSlots = null;

            if (slotStatus == ParkingSlotType.Available)
                parkingSlots = _availableParkingSlots.GetAllNodes();
            else if (slotStatus == ParkingSlotType.Occupied)
                parkingSlots = _occupiedParkingSlots.Select(x => x.Value).ToList();

            if (parkingSlots != null)
            {

                // Construct the header
                strBuilder.Append("Slot No.".PadRight(9))
                          .Append("Registration No".PadRight(19))
                          .AppendLine("Colour".PadRight(10));

                for (int i = 0; i < parkingSlots.Count(); i++)
                {

                    if (i == parkingSlots.Count() - 1)
                    {
                        //last record

                        strBuilder.Append(parkingSlots[i].SlotNo.ToString().PadRight(9))
                               .Append(parkingSlots[i].Vehicle.RegistrationNo.PadRight(19))
                               .Append(parkingSlots[i].Vehicle.Color.PadRight(10));
                    }
                    else
                    {
                        strBuilder.Append(parkingSlots[i].SlotNo.ToString().PadRight(9))
                             .Append(parkingSlots[i].Vehicle.RegistrationNo.PadRight(19))
                             .AppendLine(parkingSlots[i].Vehicle.Color.PadRight(10));
                    }
                }
            }

            return strBuilder.ToString();

        }

        public List<ParkingSlot> GetOccupiedSlotsByColour(string colour)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            var targetCarSlots = _occupiedParkingSlots.Where(x => x.Value.Vehicle.Color.CompareStringIgnoreCase(colour));

            return targetCarSlots.Select(x => x.Value).ToList();

        }

        public List<ParkingSlot> GetOccupiedSlotsByRegistrationNumber(string registrationNumber)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            var targetCarSlots = _occupiedParkingSlots.Where(x => x.Value.Vehicle.RegistrationNo.CompareStringIgnoreCase(registrationNumber));

            return targetCarSlots.Select(x => x.Value).ToList();

        }

        private bool CheckIfParkingLotIsCreated()
        {

            if (_occupiedParkingSlots.Count() == 0 && _availableParkingSlots.GetSize() == 0)
                return false;

            return true;

        }

        private void Destroy()
        {
            _availableParkingSlots.Destroy();
            _occupiedParkingSlots.Clear();
        }

        public int Count => _occupiedParkingSlots.Count();

        public bool IsCarParkFull() => _availableParkingSlots.GetSize() == 0;

    }
}
