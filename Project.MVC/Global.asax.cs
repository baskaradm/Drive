using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using AutoMapper;
using Project.MVC.App_Start;
using Project.Service.Implementations;
using Project.Service.Interfaces;
using System.Collections.Generic;
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
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest().PropertiesAutowired();

            

            builder.RegisterModule<DependencyInjectionModule>();

            
            builder.RegisterModule(new AutoMapperModule());

            

            //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());


            //Building the container
            IContainer container = builder.Build();
            //Instruct MVC to resolve controllers using the container and take care of caching container  in between
            //requests
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
