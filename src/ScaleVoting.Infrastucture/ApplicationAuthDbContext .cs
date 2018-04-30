using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class ApplicationAuthDbContext : IdentityDbContext<User>
    {
        public ApplicationAuthDbContext() : base("name=UsersDB") { }

        static ApplicationAuthDbContext()
        {
            Database.SetInitializer(new IdentityDbInit());
        }

        public static ApplicationAuthDbContext Create()
        {
            return new ApplicationAuthDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApplicationAuthDbContext>
    {
        protected override void Seed(ApplicationAuthDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(ApplicationAuthDbContext context)
        {
        }
    }
}