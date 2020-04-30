using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Project.Service.Implementations
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private VehicleContext _datacontext = new VehicleContext();


        public async Task<IEnumerable<VehicleModel>> GetVehicleModels(Filtering filters, Sorting sorting, Paging paging)
        {
           IQueryable<VehicleModel> vehicleModels = from v in  _datacontext.VehicleModels
                                select v;
            if (filters.ShouldApplyFilters())
            {
                vehicleModels = vehicleModels.Where(v => v.Name.Contains(filters.FilterBy)
                                                  || v.Abbreviation.Contains(filters.FilterBy)
                                                  || v.VehicleMakeId.ToString().Contains(filters.FilterBy));
            }

            paging.TotalCount = vehicleModels.Count();
            switch (sorting.SortBy)
            {

                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                    break;

                case "Abbreviation":
                    vehicleModels = vehicleModels.OrderBy(v => v.Abbreviation);
                    break;

                case "abbreviation_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Abbreviation);
                    break;

                case "VehicleMakeId":
                    vehicleModels = vehicleModels.OrderBy(v => v.VehicleMakeId);
                    break;

                case "vehiclemakeid_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.VehicleMakeId);
                    break;

                default:
                    vehicleModels = vehicleModels.OrderBy(v => v.Name);
                    break;

                    
            }
            return await vehicleModels.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ToListAsync();
        }

        public async Task <VehicleModel> GetVehicleModelByID(int? id)
        {
            return await _datacontext.VehicleModels.FindAsync(id);
        }


        public async Task<bool> CreateVehicleModel(VehicleModel vehicleModelToInsert)
        {
            try
            {
                _datacontext.VehicleModels.Add(vehicleModelToInsert);
                await _datacontext.SaveChangesAsync();
                return true;
            }

            catch
            {
                return false;
            }

        }

        public async Task <bool> DeleteVehicleModel(VehicleModel vehicleModel)
        {
            try
            {
                _datacontext.VehicleModels.Remove(vehicleModel);
                await _datacontext.SaveChangesAsync();
                return true;
            }

            catch
            {
                return false;
            }
        }


        public async Task <bool> EditVehicleModel(VehicleModel vehicleModelToUpdate)
        {
            try
            {
               await _datacontext.SaveChangesAsync();

                return true;
            }

            catch
            {
                return false;
            }

        }

    }

}






