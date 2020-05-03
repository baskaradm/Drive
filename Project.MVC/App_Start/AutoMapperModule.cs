using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

//Set up and register the AutoMapper IMapper interface with our Inversion of Control container, 
//in this case Autofac.  I've used an Autofac Module here to configure the mapper 
namespace Project.MVC.App_Start
{
    //Defining a module:
    public class AutoMapperModule : Module
    {
        //override to add registrations to the container.
        protected override void Load(ContainerBuilder builder)
        {
            
            //In this module we have firstly scanned through all assemblies in our project and
            //registered all our AutoMapper profiles
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();
            
            //Then we add these profiles to an AutoMapper MapperConfiguration

            //register configuration as a a single instance
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                //add profile(either resolve from container or however else we acquire them)
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            //Register our mapper
            //Here we are register the IMapper interface, which will resolve to a new instance of
            //the mapper using the registered MapperConfiguration.Once this is done we can change
            //our initial Controller(VehicleMake/VehicleModel) to inject IMapper interface and use 
            //it to map our objects
            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
            
        }
    }
}