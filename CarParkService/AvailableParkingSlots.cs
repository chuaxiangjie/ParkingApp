using CarParkService.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParkService
{
    public class AvailableParkingSlots : Heap<ParkingSlot>
    {

        public AvailableParkingSlots()
        {

        }

        public new void Add(ParkingSlot item)
        {

            item.Reset();
            base.Add(item);

        }

    }
}
