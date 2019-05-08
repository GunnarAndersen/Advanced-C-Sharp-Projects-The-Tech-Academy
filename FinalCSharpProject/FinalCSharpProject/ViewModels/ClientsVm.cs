using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCSharpProject.ViewModels
{
    public class ClientsVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string DUI { get; set; }
        public int SpeedingTickets { get; set; }
        public string FullOrLiability { get; set; }
        public int Price { get; set; }
    }
}