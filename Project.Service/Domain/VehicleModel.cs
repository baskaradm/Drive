using Project.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Domain
{
    public class VehicleModel : IVehicleModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Abbreviation { get; set; }

        public int VehicleMakeId { get; set; }
        public VehicleMake VehicleMake { get; set; }


    }
}