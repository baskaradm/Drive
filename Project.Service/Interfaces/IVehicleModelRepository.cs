using Project.Service.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModels(string sortBy, string currentFilter, string searchString);
        Task<VehicleModel>GetVehicleModelByID(int? id);
        Task<bool> CreateVehicleModel(VehicleModel vehicleModelToInsert);
        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);
        Task<bool> EditVehicleModel(VehicleModel vehicleModelToUpdate);
       
    }
}
