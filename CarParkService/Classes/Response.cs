using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingApp.Service.Classes
{
    public class Response
    {

        public bool IsSuccessful { get; set; }
         
        public string ErrorMessage { get; set; }

        public string Message { get; set; }

    }
}
