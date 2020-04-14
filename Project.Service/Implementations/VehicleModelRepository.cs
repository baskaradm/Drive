using Project.Service.Domain;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace Project.Service.Implementations
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private VehicleContext _datacontext = new VehicleContext();


        public async Task<IEnumerable<VehicleModel>> GetVehicleModels(string sortBy, string currentFilter, string searchString)
        {
            var vehicleModels = from v in  _datacontext.VehicleModels
                                select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(v => v.Name.Contains(searchString)

                                                         || v.Abbreviation.Contains(searchString)
                                                         || v.VehicleMakeId.ToString().Contains(searchString));
            }

            switch (sortBy)
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
            return await vehicleModels.ToListAsync();
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






