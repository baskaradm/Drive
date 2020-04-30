using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModels(Filtering filters, Sorting sorting, Paging paging);
        Task<VehicleModel>GetVehicleModelByID(int? id);
        Task<bool> CreateVehicleModel(VehicleModel vehicleModelToInsert);
        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);
        Task<bool> EditVehicleModel(VehicleModel vehicleModelToUpdate);
       
    }
}
