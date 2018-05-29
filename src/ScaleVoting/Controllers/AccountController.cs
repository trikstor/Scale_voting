using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using ScaleVoting.Extensions;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class AccountController : Controller
    {
        private UserValidator UserValidator { get; }

        private IAuthenticationManager AuthenticationManager =>
            HttpContext.GetOwinContext().Authentication;

        private ApplicationAuthUserManager AuthUserManager =>
            HttpContext.GetOwinContext().GetUserManager<ApplicationAuthUserManager>();

        private static string Salt => WebConfigurationManager.AppSettings["Salt"];

        public AccountController(UserValidator userValidator)
        {
            UserValidator = userValidator;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(FormUser model)
        {
            Logger.Init();
            try
            {
                if (!UserValidator.UserIsValid(model, out var message))
                {
                    ModelState.AddModelError("", message);
                    return View(model);
                }

                var user = await AuthUserManager.FindAsync(model.Email,
                                                           Cryptography.Sha256(
                                                               model.Password + Salt));

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь не найден.");
                    return View(model);
                }

                var ident =
                    await AuthUserManager.CreateIdentityAsync(
                        user, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false},
                                             ident);
                return Redirect("/ControlPanel");
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
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
            Logger.Init();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (!UserValidator.UserIsValid(model, out var message))
                {
                    ModelState.AddModelError("", message);
                    return View(model);
                }

                var user = model.ToUser(Salt);
                var result =
                    await AuthUserManager.CreateAsync(
                        user, Cryptography.Sha256(model.Password + Salt));

                if (result.Succeeded)
                {
                    return Redirect("/Account/Login");
                }

                AddErrorsFromResult(result);

                return View(model);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}