using Libraries.Extensions;
using ParkingApp.Service.Abstracts;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Exceptions;
using ParkingApp.Service.Interfaces;
using ParkingApp.Service.Models;
using ParkingApp.Service.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingApp.Service.Classes.Implementations
{
    public class SmartCarParkSystem : ISmartCarParkPlugin
    {

        #region Fields

        /// <summary>
        /// <K,V> - <slotNo, ParkingSlot>
        /// Gets or sets the occupied parking slots
        /// </summary>
        Dictionary<int, ParkingSlot> _occupiedParkingSlots { get; set; }

        /// <summary>
        /// Gets or sets the available parking slots
        /// </summary>
        AvailableParkingSlots _availableParkingSlots { get; set; }

        public int OccupiedSlotsCount => _occupiedParkingSlots.Count();

        public int AvailableSlotsCount => _availableParkingSlots.GetSize();

        #endregion

        #region Ctor

        public SmartCarParkSystem()
        {

            //Initialize ParkingSlots
            _occupiedParkingSlots = new Dictionary<int, ParkingSlot>();

            _availableParkingSlots = new AvailableParkingSlots();

        }

        #endregion

        #region Public Methods

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

        public Response RegisterVehicleArrival(Vehicle vehicle)
        {

            Response response = new Response();

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            //Check if registrationNo exists in occupied parking slots
            var existingOccupiedSlot = _occupiedParkingSlots.SingleOrDefault(x => x.Value.Vehicle.RegistrationNo.CompareStringIgnoreCase(vehicle.RegistrationNo));

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

                assignedSlot.AssignedVehicle(vehicle, ParkingSlotType.Occupied);

                _occupiedParkingSlots.Add(assignedSlot.SlotNo, assignedSlot);

                response.Message = $"Allocated slot number: {assignedSlot.SlotNo}";
                response.IsSuccessful = true;
            }
            else
            {
                // carpark is full
                response.ErrorMessage = $"Sorry, parking lot is full";
                response.IsSuccessful = false;
            }

            return response;
        }

        public Response RegisterVehicleExit(int slotNo)
        {

            Response response = new Response();

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

            response.Message = $"Slot number {slotNo} is free";
            response.IsSuccessful = true;

            return response;

        }

        public Response GetCarSlotsStatus(ParkingSlotType slotStatus)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            Response response = new Response();

            StringBuilder strBuilder = new StringBuilder();
            IList<ParkingSlot> parkingSlots = null;

            if (slotStatus == ParkingSlotType.Available)
                parkingSlots = _availableParkingSlots.GetAllNodes();
            else if (slotStatus == ParkingSlotType.Occupied)
                parkingSlots = _occupiedParkingSlots.Select(x => x.Value).ToList();

            if (parkingSlots != null)
            {

                // Construct the header
                strBuilder.Append("Slot No.".PadRight(12))
                          .Append("Registration No".PadRight(19))
                          .AppendLine("Colour");

                for (int i = 0; i < parkingSlots.Count(); i++)
                {

                    if (i == parkingSlots.Count() - 1)
                    {
                        //last record
                        strBuilder.Append(parkingSlots[i].SlotNo.ToString().PadRight(12))
                               .Append(parkingSlots[i].Vehicle.RegistrationNo.PadRight(19))
                               .Append(parkingSlots[i].Vehicle.Color);
                    }
                    else
                    {
                        strBuilder.Append(parkingSlots[i].SlotNo.ToString().PadRight(12))
                             .Append(parkingSlots[i].Vehicle.RegistrationNo.PadRight(19))
                             .Append(parkingSlots[i].Vehicle.Color);

                        strBuilder.Append(Environment.NewLine);
                    }
                }
            }

            response.IsSuccessful = true;
            response.Message = strBuilder.ToString();

            return response;

        }

        public IList<ParkingSlot> GetOccupiedSlotsByColour(string colour)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            var targetCarSlots = _occupiedParkingSlots.Where(x => x.Value.Vehicle.Color.CompareStringIgnoreCase(colour));

            return targetCarSlots.Select(x => x.Value).ToList();

        }

        public IList<ParkingSlot> GetOccupiedSlotsByRegistrationNumber(string registrationNumber)
        {

            if (!CheckIfParkingLotIsCreated())
                throw new ParkingSystemException("Please create parking lot first");

            var targetCarSlots = _occupiedParkingSlots.Where(x => x.Value.Vehicle.RegistrationNo.CompareStringIgnoreCase(registrationNumber));

            return targetCarSlots.Select(x => x.Value).ToList();

        }

        public bool IsCarParkFull() => _availableParkingSlots.GetSize() == 0;

        #endregion

        #region Private Methods

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

        #endregion

    }
}
