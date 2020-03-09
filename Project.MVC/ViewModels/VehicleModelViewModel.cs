using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        
        public int ID { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public int VehicleMakeId { get; set; }
    }
}