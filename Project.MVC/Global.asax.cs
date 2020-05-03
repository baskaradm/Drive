using Autofac;
using Autofac.Integration.Mvc;
using Project.MVC.App_Start;
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

            //Register DIModule
            builder.RegisterModule<DependencyInjectionModule>();

            //Register AutoMapperModule
            builder.RegisterModule<AutoMapperModule>();
            
            //Register all  MVC controllers automatically and we need tell Autofac dispose yourself 
            // when the web requests is done and release classes that you may be holding onto
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            //Building the container
            IContainer container = builder.Build();

            //Instruct MVC to resolve controllers using the container and take care of caching container  in between
            //requests
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
