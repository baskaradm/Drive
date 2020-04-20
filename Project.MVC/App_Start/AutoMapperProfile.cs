using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service.Domain;

namespace Project.MVC.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
        
    }
}