using CarParkService.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService.Classes
{
    public sealed class CarParkService
    {

        #region Fields

        private static readonly Lazy<CarParkService>
            lazy = new Lazy<CarParkService>
                (() => new CarParkService());


        public CarPark CarParkPremise { get; private set; }

        private Dictionary<string, Func<string[], string>> _commandDict;

        public static CarParkService Instance { get { return lazy.Value; } }

        #endregion

        #region Ctor
        private CarParkService()
        {
            CarParkPremise = new CarPark();

            _commandDict = new Dictionary<string, Func<string[], string>>();

            _commandDict.Add(Constants.CommandAction.CREATE_PARKING_LOT, CreateParkingLot);
            _commandDict.Add(Constants.CommandAction.PARK, AssignCarSlot);
            _commandDict.Add(Constants.CommandAction.LEAVE, ReleaseCarSlot);

        }

        #endregion

        #region Public Methods

        public string Execute(string command)
        {

            string[] tokens = command.Split(' ');

            if (tokens.Length == 0)
                return "Invalid input";

            string actionCommand = tokens[0];

            if (_commandDict.ContainsKey(actionCommand)) 
            {
                _commandDict[actionCommand](tokens);
            }

            return "";

        }

        #endregion

        #region Private Methods

        #region Create Parking Lot

        private string CreateParkingLot(string[] tokens)
        {
       
            if (tokens.Length != 2)
                return "Invalid Format";

            if (Int32.TryParse(tokens[1], out int numberofSlots))
            {
                if (numberofSlots <= 0)
                    return "Invalid Format";

                //Invoke Create Parking Lot
                CarParkPremise.CreateParkingSlots(numberofSlots);
            }
            else
            {
                return "Invalid Format";
            }

            return "true";
        }

        #endregion

        #region Assign Car Slot

        private string AssignCarSlot(string[] tokens)
        {

            if (tokens.Length != 3)
                return "Invalid Format";

            string carPlate = tokens[1];
            string color = tokens[2];

            if (String.IsNullOrEmpty(carPlate))
                return "Car Plate cannot be empty";

            if (String.IsNullOrEmpty(color))
                return "Color cannot be empty";

            //Invoke Park Car
            return CarParkPremise.AssignCarSlot(carPlate, color);

        }

        #endregion

        #region Release Car Slot

        private string ReleaseCarSlot(string[] tokens)
        {

            if (tokens.Length != 2)
                return "Invalid Format";

            if (Int32.TryParse(tokens[1], out int slotNo))
            {
                if (slotNo <= 0)
                    return "Invalid Format";

                //Invoke Leave Car Slot
                return CarParkPremise.ReleaseCarSlot(slotNo);
            }
            else
            {
                return "Invalid Format";
            }

        }

        #endregion

        #endregion

    }
}
