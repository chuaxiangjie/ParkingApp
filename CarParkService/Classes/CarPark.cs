using CarParkService.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CarParkService.Classes
{
    public class CarPark
    {

        List<ParkingSlot> ParkingSlots { get; set; }


        public CarPark()
        {

            //Initialize ParkingSlots
            ParkingSlots = new List<ParkingSlot>();

        }


        public void Init(int number)
        {

            if (number <= 0)
                return;

            for (int i = 1; i < number + 1; i++)
            {
                ParkingSlot newSlot = new ParkingSlot();
                ParkingSlots.Add(newSlot);
            }

        }

        public int Count => ParkingSlots.Count();

        public bool IsCarParkFull()
        {
            return !ParkingSlots.Any(x => x.Status == ParkingSlotType.Available);
        }

    }
}
