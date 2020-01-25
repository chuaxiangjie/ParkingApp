using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService.Enums
{

    /// <summary>
    /// Represents a parking slot type
    /// </summary>
    [Flags]
    public enum ParkingSlotType
    {
        /// <summary>
        /// Slot is empty
        /// </summary>
        Available = 1,

        /// <summary>
        /// Slot is occupied
        /// </summary>
        Occupied = 2,
    }

}
