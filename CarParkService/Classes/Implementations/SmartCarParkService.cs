using Libraries;
using ParkingApp.Service.Abstracts;
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
    public class SmartCarParkService : ServiceBase<SmartCarParkSystem>, IClientCarParkService
    {

        #region Fields

        private Dictionary<string, Func<string[], string>> _commandDict;

        protected override SmartCarParkSystem _carParkSystem { get; set; }

        #endregion

        #region Ctor
        public SmartCarParkService()
        {

            _carParkSystem = new SmartCarParkSystem();

            _commandDict = new Dictionary<string, Func<string[], string>>();

            _commandDict.Add(Constants.CommandAction.CREATE_PARKING_LOT, CreateParkingLot);
            _commandDict.Add(Constants.CommandAction.PARK, AssignCarSlot);
            _commandDict.Add(Constants.CommandAction.LEAVE, ReleaseCarSlot);
            _commandDict.Add(Constants.CommandAction.STATUS, GetCarParkSlotsStatus);
            _commandDict.Add(Constants.CommandAction.GET_REGISTRATION_NUMBERS_FOR_CARS_BY_COLOUR, GetCarRegistrationNumbersByColour);
            _commandDict.Add(Constants.CommandAction.GET_SLOT_NUMBERS_FOR_CARS_BY_COLOUR, GetSlotNumbersForCarsByColour);
            _commandDict.Add(Constants.CommandAction.GET_SLOT_NUMBERS_BY_REGISTRATION_NUMBER, GetSlotNumberByRegistrationNumber);
        }

        #endregion

        #region Public Methods

        public string Execute(string command)
        {

            string[] tokens = command.Trim().Split(' ');

            if (tokens.Length == 0)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string actionCommand = tokens[0];

            if (_commandDict.ContainsKey(actionCommand))
            {
                return _commandDict[actionCommand](tokens);
            }
            else
            {
                throw new ParkingSystemException("Unrecognized command");
            }
        }

        #endregion

        #region Private Methods

        private string CreateParkingLot(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            if (Int32.TryParse(tokens[1], out int numberofSlots))
            {
                if (numberofSlots <= 0)
                    throw new ParkingSystemException("Number of slots should be more than 0");

                //Invoke Create Parking Lot
                _carParkSystem.CreateParkingSlots(numberofSlots);

                return $"Created a parking lot with {numberofSlots} slots";
            }
            else
            {
                throw new ParkingSystemException("Slot no must be integer");
            }

        }

        private string AssignCarSlot(string[] tokens)
        {

            if (tokens.Length != 3)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string carPlate = tokens[1];
            string color = tokens[2];

            if (String.IsNullOrEmpty(carPlate))
                throw new ParkingSystemException("Car Plate cannot be empty");

            if (String.IsNullOrEmpty(color))
                throw new ParkingSystemException("Color cannot be empty");

            //Create instance of Car
            Vehicle car = new Car(carPlate, color);

            //Invoke Park Car
            var response = _carParkSystem.RegisterVehicleArrival(car);

            if (response.IsSuccessful)
            {
                return response.Message;
            }
            else
            {
                return response.ErrorMessage;
            }

        }

        private string ReleaseCarSlot(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            if (Int32.TryParse(tokens[1], out int slotNo))
            {
                if (slotNo <= 0)
                    throw new ParkingSystemException("Slot no must be more than 0");

                //Invoke Release Car Slot
                var response = _carParkSystem.RegisterVehicleExit(slotNo);

                if (response.IsSuccessful)
                    return $"Slot number {slotNo} is free";
                else
                    return response.ErrorMessage;
            }
            else
            {
                throw new ParkingSystemException("Slot no must be integer");
            }

        }

        private string GetCarParkSlotsStatus(string[] tokens)
        {

            if (tokens.Length != 1)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            var response = _carParkSystem.GetCarSlotsStatus(Enums.ParkingSlotType.Occupied);

            if (response.IsSuccessful)
                return response.Message;
            else
                return response.ErrorMessage;

        }

        private string GetCarRegistrationNumbersByColour(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string targetColour = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByColour(targetColour).ToList();

            if (occupiedSlots != null && occupiedSlots.Count() > 0)
            {
                return String.Join(", ", occupiedSlots.Select(x => x.Vehicle.RegistrationNo));
            }
            else
            {
                return $"Not found";
            }

        }

        private string GetSlotNumbersForCarsByColour(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string targetColour = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByColour(targetColour).ToList();

            if (occupiedSlots == null || occupiedSlots.Count() == 0)
                return "Not found";

            return String.Join(", ", occupiedSlots.Select(x => x.SlotNo));

        }

        private string GetSlotNumberByRegistrationNumber(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string registrationNumber = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByRegistrationNumber(registrationNumber).ToList();

            if (occupiedSlots == null || occupiedSlots.Count() == 0)
                return "Not found";

            return String.Join(", ", occupiedSlots.Select(x => x.SlotNo));

        }

        #endregion

    }
}
