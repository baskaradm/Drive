using Project.Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Domain
{
    public class VehicleMake : IVehicleMake
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