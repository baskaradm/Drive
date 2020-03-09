using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service
{
    public class VehicleMake 
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Abbreviation { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
        
    }
}