using System;
using System.Runtime.Serialization;

namespace ParkingApp.Service.Exceptions
{
    [Serializable]
    public class ParkingSystemException : Exception
    {
        public ParkingSystemException()
        {
        }

        public ParkingSystemException(string message) : base(message)
        {
        }

        public ParkingSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParkingSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}