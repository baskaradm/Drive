using Project.Service.Domain;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Implementations
{

    public class VehicleModelService : IVehicleModelService
    {
        private IValidationDictionary _validationDictionary;


        private readonly IVehicleModelRepository _vehicleModelRepository;


        public VehicleModelService(IValidationDictionary validationDictionary, IVehicleModelRepository vehicleModelRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleModelRepository = vehicleModelRepository;
        }


        protected bool ValidateVehicleModel(VehicleModel vehicleModelToValidate)
        {
            if (vehicleModelToValidate.Name == null)
            {
                _validationDictionary.AddError("Name", "Name is required.");
            }

            if (vehicleModelToValidate.Abbreviation == null)
            {

               _validationDictionary.AddError("Abbreviation", "Abbreviation is required.");
            }

            return _validationDictionary.IsValid;

        }

        public async Task <IEnumerable<VehicleModel>>GetVehicleModelsAsync(string sortBy, string currentFilter, string searchString)
        {

            return await _vehicleModelRepository.GetVehicleModels(sortBy, currentFilter,  searchString);
        }

        public async Task <VehicleModel>GetVehicleModelByIDAsync(int? id)
        {

            return await _vehicleModelRepository.GetVehicleModelByID(id);
        }


        public async Task <bool> CreateVehicleModel(VehicleModel vehicleModelToInsert)
        {
            // Validation logic
            if (!ValidateVehicleModel(vehicleModelToInsert))
                return false;

            // Database logic
            bool result = await _vehicleModelRepository.CreateVehicleModel(vehicleModelToInsert);
            return result;

        }


        public async Task <bool> EditVehicleModel(VehicleModel vehicleModelToUpdate)
        {

            bool result = await _vehicleModelRepository.EditVehicleModel(vehicleModelToUpdate);
            return result;
        }
        


        public async Task <bool> DeleteVehicleModel(VehicleModel vehicleModel)
        {

            bool result = await _vehicleModelRepository.DeleteVehicleModel(vehicleModel);
            return result;

        }
        
    }
}
