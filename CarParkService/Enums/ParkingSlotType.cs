using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService.Enums
{

    /// <summary>
    /// Represents a parking slot type
    /// </summary>
    public enum ParkingSlotType
    {
        /// <summary>
        /// Slot is empty
        /// </summary>
        Available = 0,

        /// <summary>
        /// Slot is reserved
        /// </summary>
        Reserved = 1,

        /// <summary>
        /// Slot is occupied
        /// </summary>
        Occupied = 1,
    }

}
