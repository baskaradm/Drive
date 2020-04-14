using Autofac;
using Autofac.Integration.Mvc;
using Project.Service;
using Project.Service.Implementations;
using Project.Service.Interfaces;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Project.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Creating  ContainerBuilder class
            ContainerBuilder builder = new ContainerBuilder();

            //Register all  MVC controllers automatically
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            //Register Components 
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();
            builder.RegisterType<ModelStateWrapper>().As<IValidationDictionary>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
            builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>();
           
            //Building the container
            IContainer container = builder.Build();
           
            //Instruct MVC to resolve controllers using the container and take care of caching container between
            //two requests
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
