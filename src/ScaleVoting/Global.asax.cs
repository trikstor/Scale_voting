using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using EmailValidation;
using System;

namespace ScaleVoting
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            builder.RegisterType<EmailValidator>();
            builder.RegisterType<PollValidator>();
            builder.RegisterType<UserValidator>();

            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Write(@"
                <html>
                <head>
                <title>Некоректный пользовательский ввод</title>
                <script language='JavaScript'><!--
                function back() { history.go(-1); } //-->
                </script>
                </head>
                <body style='font-family: Arial, Sans-serif;'>
                <h1>Извините, но...</h1>
                <p>HTML, как и XSS и SQL инъекции не допустимы на этом сайте. 
                Увы и ах !</p>
                <p><a href='javascript:back()'>Вернуться</a></p>
                </body>
                </html>
                ");
                Response.End();
            }
        }

    }
}