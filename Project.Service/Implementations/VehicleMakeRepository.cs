using Project.Service.Domain;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace Project.Service.Implementations
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private VehicleContext _datacontext = new VehicleContext();


       public async Task <IEnumerable<VehicleMake>> GetVehicleMakes(string sortBy, string currentFilter, string searchString)
       {

            var vehicleMakes = from v in _datacontext.VehicleMakes
                               select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchString)
                                               || v.Abbreviation.Contains(searchString));
            }
            switch (sortBy)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(v => v.Name);
                    break;
                case "Abbreviation":
                    vehicleMakes = vehicleMakes.OrderBy(v => v.Abbreviation);
                    break;
                case "abbreviation_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(v => v.Abbreviation);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(v => v.Name);
                    break;
            }


            return await vehicleMakes.ToListAsync();

        }


        public async Task <VehicleMake> GetVehicleMakeByID(int? id)
        {

            return await _datacontext.VehicleMakes.FindAsync(id);

        }


        public async Task <bool> CreateVehicleMake(VehicleMake vehicleMakeToInsert)
        {
            try
            {
                _datacontext.VehicleMakes.Add(vehicleMakeToInsert);
                await _datacontext.SaveChangesAsync();

                return true;
            }

            catch
            {
                return false;
            }
        }

        public async Task <bool> DeleteVehicleMake(VehicleMake vehicleMake)
        {
            try
            {
                _datacontext.VehicleMakes.Remove(vehicleMake);
                await _datacontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task <bool> EditVehicle(VehicleMake vehicleMakeToUpdate)
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





