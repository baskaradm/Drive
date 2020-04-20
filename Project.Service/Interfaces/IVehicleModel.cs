using Project.Service.Domain;

namespace Project.Service.Interfaces
{
    public interface IVehicleModel
    {
        int ID { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
        int VehicleMakeId { get; set; }
        VehicleMake VehicleMake { get; set; }
    }
}
