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
            lazy =
            new Lazy<CarParkService>
                (() => new CarParkService());


        public CarPark CarParkPremise { get; private set; }

        public static CarParkService Instance { get { return lazy.Value; } }

        #endregion

        #region Ctor
        private CarParkService()
        {
            CarParkPremise = new CarPark();
        }

        #endregion

        #region Public Methods

        public void Execute(string command)
        {

            string[] tokens = command.Split(' ');

            if (tokens.Length == 0)
                return;

            string actionCommand = tokens[0];

            if (actionCommand.CompareStringIgnoreCase("create_parking_lot"))
            {

                #region Create Parking Lot

                if (tokens.Length > 2)
                    return;

                if (Int32.TryParse(tokens[1], out int numberofSlots))
                {
                    if (numberofSlots <= 0)
                        return;

                    //Invoke Create Parking Lot
                    CreateParkingLot(numberofSlots);
                }
                else
                {
                    return;
                }

                #endregion

            }
            else if (actionCommand.CompareStringIgnoreCase("park"))
            {





            }
            else if (actionCommand.CompareStringIgnoreCase("leave"))
            {

            }
            else if (actionCommand.CompareStringIgnoreCase("status"))
            {

            }
            else if (actionCommand.CompareStringIgnoreCase("registration_numbers_for_cars_with_colour"))
            {

            }
            else if (actionCommand.CompareStringIgnoreCase("slot_number_for_registration_number"))
            {

            }
        }

        #endregion

        #region Private Methods
        private void CreateParkingLot(int numberSlots)
        {
            CarParkPremise.Init(numberSlots);
        }

        #endregion

    }
}
