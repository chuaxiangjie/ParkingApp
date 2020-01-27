using ParkingApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Abstracts
{
    public abstract class ServiceBase<T> where T : ICarParkSystem
    {

        protected abstract T _carParkSystem { get; set; }

    }
}
