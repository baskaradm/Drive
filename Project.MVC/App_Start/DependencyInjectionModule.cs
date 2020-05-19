using Autofac;
using Autofac.Features.ResolveAnything;
using Project.Service;
using Project.Service.Implementations;
using Project.Service.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Project.MVC.App_Start
{ 

    public class DependencyInjectionModule : Module
    {
        //ContainterBuilder is being received in this method's argument
        protected override void Load(ContainerBuilder builder)
        {
            // Here we need to register all the components we used
            //that involve DI or we solved through DI


            //We use RegisterAssemblyTypes Method and say it it scan the assembly that contains 
            //VehicleMakeRepository and we also registers a VehicleModelRepository 
            //And in future if we keep data repositories in the same assembly this is 
            //only registration instruction line we will need

            //We won't to add everything in the assembly and we do that with help of lambda 
            //expressions
            builder.RegisterAssemblyTypes(typeof(VehicleMakeRepository).Assembly)
                //Add lambda Where clause to limit the types in assembly that end with the
                //word "Repository"
                .Where(v => v.Name.EndsWith("Repository"))
                //Give it interface association
                //t is current type in scan and merely obtain the interfaces for that 
                //type and if the list is non-null(?), we are grabing first interface
                //in the list where the name is the same as the type name, but with letter
                //I in front
                .As(v => v.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + v.Name))
                .InstancePerRequest();

            //Same idea for VehicleMakeService
            builder.RegisterAssemblyTypes(typeof(VehicleMakeService).Assembly)
               .Where(v => v.Name.EndsWith("Service"))
               .As(v => v.GetInterfaces()?.FirstOrDefault(
                   i => i.Name == "I" + v.Name))
               .InstancePerRequest();

            //Default registrations
            //Note here => we cannot register this component as the above components because class
            //and the interface does not have the same name
            builder.RegisterType<ModelStateWrapper>().As<IValidationDictionary>().InstancePerRequest();

            //// Register instance of object we create
            var output = new ModelStateDictionary();
            builder.RegisterInstance(output).As<ModelStateDictionary>();

            //Context registration
            builder.RegisterType<VehicleContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            
        }
    }

}      
    
