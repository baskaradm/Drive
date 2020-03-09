using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service
{
    public class VehicleModel
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