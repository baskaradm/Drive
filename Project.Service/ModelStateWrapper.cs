using Project.Service.Interfaces;
using System.Web.Mvc;


namespace Project.Service
{
    public class ModelStateWrapper : IValidationDictionary
    {
        public ModelStateDictionary _modelState  { get;  set;}

        public  ModelStateWrapper(){}

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get {
                return _modelState.IsValid;
            }
        }

        
    }
}
