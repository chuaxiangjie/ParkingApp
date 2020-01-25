using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries
{
    public class Constants
    {

        public static class CommandAction
        {
            public const string CREATE_PARKING_LOT = "create_parking_lot";
            public const string PARK = "park";
            public const string LEAVE = "leave";
            public const string STATUS = "status";
            public const string REGISTRATION_NUMBERS_FOR_CARS_WITH_COLOUR = "registration_numbers_for_cars_with_colour";
            public const string SLOT_NUMBER_FOR_REGISTRATION_NUMBER = "slot_number_for_registration_number";
        }

        public static class ErrorHandling
        {

            public const string INVALID_NUMBER_OF_INPUT_PARAMETERS = "Invalid number of input parameters";


        }
    }
}
