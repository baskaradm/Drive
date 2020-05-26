using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service.Domain;

//Registering objects to be mapped with AutoMapper  using  a Profile and Here we have created 
//mappings for the objects below.

namespace Project.MVC.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        }
        
    }
}