using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakes(Filtering filters, Sorting sorting, Paging paging);
        Task<VehicleMake> GetVehicleMakeByID(int? id);
        Task<bool> CreateVehicleMake(VehicleMake vehicleMakeToInsert);
        Task<bool> DeleteVehicleMake(VehicleMake vehicleMake);
        Task<bool> EditVehicle(VehicleMake vehicleMakeToUpdate);
        
    }
}
