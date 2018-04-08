using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // MVC setup documentation here:
            // http://autofac.readthedocs.io/en/latest/integration/mvc.html
            // WCF setup documentation here:
            // http://autofac.readthedocs.io/en/latest/integration/wcf.html
            //
            // First we'll register the MVC/WCF stuff...
            var builder = new ContainerBuilder();
            
            // MVC - Register your MVC controllers.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
            
            // Register application dependencies.
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            
            /*
            builder.RegisterType<MarkdownToHtmlProcessor>().As<IPreprocessing>();
            builder.RegisterType<Shielding>().As<IPreprocessing>();
            builder.RegisterType<FieldValidator>().As<IFieldValidator>();
            builder.RegisterType<FieldPreprocessor>().As<IPreprocessor>();
            */
            
            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}