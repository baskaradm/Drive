using Project.Service.Domain;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Implementations
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IValidationDictionary _validationDictionary;

        private readonly IVehicleMakeRepository _vehicleMakeRepository;


        public VehicleMakeService(IValidationDictionary validationDictionary, IVehicleMakeRepository vehicleMakeRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleMakeRepository = vehicleMakeRepository;
        }


        protected bool ValidateVehicleMake(VehicleMake vehicleMakeToValidate)
        {
            if (vehicleMakeToValidate.Name == null)
            {
                _validationDictionary.AddError("Name", "Name is required.");
            }

            if (vehicleMakeToValidate.Abbreviation == null)
            {
                _validationDictionary.AddError("Abbreviation", "Abbreviation is required.");
            }

            return _validationDictionary.IsValid;

        }


        public async Task <IEnumerable<VehicleMake>> GetVehicleMakesAsync(string sortBy, string currentFilter, string searchString)
        {

            return await _vehicleMakeRepository.GetVehicleMakes(sortBy,  currentFilter,  searchString);
        }

        public async Task <VehicleMake> GetVehicleMakeByIDAsync(int? id)
        {
            return await _vehicleMakeRepository.GetVehicleMakeByID(id);
        }


        public async Task <bool> CreateVehicleMake(VehicleMake vehicleMakeToInsert)
        {
            // Validation logic
            if (!ValidateVehicleMake(vehicleMakeToInsert))
                return false;

            // Database logic
            bool result = await _vehicleMakeRepository.CreateVehicleMake(vehicleMakeToInsert);
            return result;

        }

        public async Task <bool> EditVehicle(VehicleMake vehicleMakeToUpdate)
        {
            bool result = await _vehicleMakeRepository.EditVehicle(vehicleMakeToUpdate);
            return result;
        }

        public async Task <bool> DeleteVehicleMake(VehicleMake vehicleMake)
        {

            bool result = await _vehicleMakeRepository.DeleteVehicleMake(vehicleMake);
            return result;
        }
    }      
}
