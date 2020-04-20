using Autofac;
using Project.Service;
using Project.Service.Domain;
using Project.Service.Implementations;
using Project.Service.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Project.MVC.App_Start
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(VehicleMakeRepository).Assembly)
                .Where(v => v.Name.EndsWith("Repository"))
                .As(v => v.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + v.Name))
                .InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(VehicleModelRepository).Assembly)
                .Where(v => v.Name.EndsWith("Repository"))
                .As(v => v.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + v.Name))
                .InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(VehicleMakeService).Assembly)
               .Where(v => v.Name.EndsWith("Service"))
               .As(v => v.GetInterfaces()?.FirstOrDefault(
                   i => i.Name == "I" + v.Name))
               .InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(VehicleModelService).Assembly)
              .Where(v => v.Name.EndsWith("Service"))
              .As(v => v.GetInterfaces()?.FirstOrDefault(
                  i => i.Name == "I" + v.Name))
              .InstancePerRequest();

            builder.RegisterType<VehicleMake>().As<IVehicleMake>().InstancePerRequest();
            builder.RegisterType<VehicleModel>().As<IVehicleModel>().InstancePerRequest();

            builder.RegisterType<ModelStateWrapper>().As<IValidationDictionary>().InstancePerRequest();
            //builder.RegisterType<ModelStateDictionary>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ModelStateDictionary>().As<ModelStateDictionary>().InstancePerRequest();




        }
    }


}      
    
