using ParkingApp.Service.Abstracts;
using ParkingApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Classes.Implementations
{
    public class TraditionalCarParkService : ServiceBase<TradtionalCarParkSystem>, IClientCarParkService
    {
        protected override TradtionalCarParkSystem _carParkSystem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Execute(string command)
        {
            throw new NotImplementedException();
        }
    }
}
