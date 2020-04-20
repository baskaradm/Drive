using Project.Service.Domain;
using System.Collections.Generic;

namespace Project.Service.Interfaces
{
    public interface IVehicleMake
    {

        int ID { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
