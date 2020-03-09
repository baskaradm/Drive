using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.Service;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

    }
}