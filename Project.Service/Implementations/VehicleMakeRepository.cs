using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Project.Service.Implementations
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private  readonly VehicleContext _datacontext = new VehicleContext();


       public async Task <IEnumerable<VehicleMake>> GetVehicleMakes(Filtering filters, Sorting sorting, Paging paging)
       {
           
           IQueryable<VehicleMake> vehicleMakes = from v in _datacontext.VehicleMakes
                               select v;

            //Find/Filter vehicle by name or abbreviation
            if (filters.ShouldApplyFilters())
            {
                vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(filters.FilterBy)
                                               || v.Abbreviation.Contains(filters.FilterBy));
            }
            
            paging.TotalCount = vehicleMakes.Count();

            //Sorting
            switch (sorting.SortBy)
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

            
            return await vehicleMakes.Skip(paging.ItemsToSkip).Take(paging.NumberOfObjectsPerPage).ToListAsync();

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





