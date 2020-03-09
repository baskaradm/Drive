using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service;

namespace Project.MVC.App_Start
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
    }
}