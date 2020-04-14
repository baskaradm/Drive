using Project.Service.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelsAsync(string sortBy, string currentFilter, string searchString);
        Task<VehicleModel> GetVehicleModelByIDAsync(int? id);
        Task<bool> CreateVehicleModel(VehicleModel vehicleModelToInsert);
        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);
        Task<bool> EditVehicleModel(VehicleModel vehicleModelToUpdate);
        
    }
}
