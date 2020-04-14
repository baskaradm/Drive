using Project.Service.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakesAsync(string sortBy, string currentFilter, string searchString);
        Task<VehicleMake>GetVehicleMakeByIDAsync(int? id);
        Task<bool> CreateVehicleMake(VehicleMake vehicleMakeToInsert);
        Task<bool> DeleteVehicleMake(VehicleMake vehicleMake);
        Task<bool> EditVehicle(VehicleMake vehicleMakeToUpdate);
       
    }
}
