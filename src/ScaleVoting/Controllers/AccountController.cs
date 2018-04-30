using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ScaleVoting.Domains;
using ScaleVoting.Extensions;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private ApplicationAuthUserManager AuthUserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationAuthUserManager>();
        private string Salt => WebConfigurationManager.AppSettings["Salt"];

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormUser model)
        {
            var user = await AuthUserManager.FindAsync(model.Email, Cryptography.Sha256(model.Password + Salt));

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                var ident = await AuthUserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(
                    new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                return Redirect("/ControlPanel");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Redirect("/Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(FormUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, PasswordHash = Cryptography.Sha256(model.Password + Salt) };
                var result = await AuthUserManager.CreateAsync(user, Cryptography.Sha256(model.Password + Salt));
                if (result.Succeeded)
                {
                    return Redirect("/Account/Login");
                }
                AddErrorsFromResult(result);
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}