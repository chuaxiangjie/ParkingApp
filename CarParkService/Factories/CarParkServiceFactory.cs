using ParkingApp.Service.Classes.Implementations;
using ParkingApp.Service.Enums;
using ParkingApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Factories
{
    public static class CarParkServiceFactory
    {
        public static IClientCarParkService Build(CarParkModelType type)
        {
            switch (type)
            {
                case CarParkModelType.TraditionalModel:
                    return new TraditionalCarParkService();
                case CarParkModelType.SmartModel:
                    return new SmartCarParkService();
                default:
                    return new SmartCarParkService();

            }
        }
    }
}
