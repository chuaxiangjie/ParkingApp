using CarParkService.Exceptions;
using CarParkService.Models;
using Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkService.Classes
{
    public sealed class CarParkService
    {

        #region Fields

        private static readonly Lazy<CarParkService>
            lazy = new Lazy<CarParkService>
                (() => new CarParkService());


        internal CarParkSystem _carParkSystem { get; private set; }

        private Dictionary<string, Func<string[], string>> _commandDict;

        public static CarParkService Instance { get { return lazy.Value; } }

        #endregion

        #region Ctor
        private CarParkService()
        {
            _carParkSystem = new CarParkSystem();

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

        #region Create Parking Lot

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

        #endregion

        #region Assign Car Slot

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

            //Invoke Park Car
            return _carParkSystem.AssignCarSlot(carPlate, color);

        }

        #endregion

        #region Release Car Slot

        private string ReleaseCarSlot(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            if (Int32.TryParse(tokens[1], out int slotNo))
            {
                if (slotNo <= 0)
                    throw new ParkingSystemException("Slot no must be more than 0");

                //Invoke Release Car Slot
                _carParkSystem.ReleaseCarSlot(slotNo);

                return $"Slot number {slotNo} is free";
            }
            else
            {
                throw new ParkingSystemException("Slot no must be integer");
            }

        }

        #endregion

        #region Get car park slots Status

        private string GetCarParkSlotsStatus(string[] tokens)
        {

            if (tokens.Length != 1)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            return _carParkSystem.GetCarSlotsStatus(Enums.ParkingSlotType.Occupied);

        }

        #endregion

        #region Get registration_numbers_for_cars_with_colour 

        private string GetCarRegistrationNumbersByColour(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string targetColour = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByColour(targetColour);

            if (occupiedSlots != null && occupiedSlots.Count() > 0)
            {
                return String.Join(", ", occupiedSlots.Select(x => x.Vehicle.RegistrationNo));
            }
            else
            {
                return $"Not found";
            }

        }

        #endregion

        #region Get slot_numbers_for_cars_with_colour 

        private string GetSlotNumbersForCarsByColour(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string targetColour = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByColour(targetColour);

            if (occupiedSlots == null || occupiedSlots.Count() == 0)
                return "Not found";

            return String.Join(", ", occupiedSlots.Select(x => x.SlotNo));

        }

        #endregion

        #region Get slot_number_for_registration_number 

        private string GetSlotNumberByRegistrationNumber(string[] tokens)
        {

            if (tokens.Length != 2)
                throw new ParkingSystemException(Constants.ErrorHandling.INVALID_NUMBER_OF_INPUT_PARAMETERS);

            string registrationNumber = tokens[1];

            List<ParkingSlot> occupiedSlots = _carParkSystem.GetOccupiedSlotsByRegistrationNumber(registrationNumber);

            if (occupiedSlots == null || occupiedSlots.Count() == 0)
                return "Not found";

            return String.Join(", ", occupiedSlots.Select(x => x.SlotNo));

        }

        #endregion

        #endregion

    }
}
