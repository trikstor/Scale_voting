using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using ScaleVoting.Infrastucture;

namespace ScaleVoting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationAuthDbContext.Create);
            app.CreatePerOwinContext<ApplicationAuthUserManager>(ApplicationAuthUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}