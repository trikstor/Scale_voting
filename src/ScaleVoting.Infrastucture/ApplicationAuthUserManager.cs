using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class ApplicationAuthUserManager : UserManager<User>
    {
        public ApplicationAuthUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationAuthUserManager Create(IdentityFactoryOptions<ApplicationAuthUserManager> options,
            IOwinContext context)
        {
            var db = context.Get<ApplicationAuthDbContext>();
            ApplicationAuthUserManager manager = new ApplicationAuthUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}