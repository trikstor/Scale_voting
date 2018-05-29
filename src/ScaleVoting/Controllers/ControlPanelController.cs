using System;
using System.Linq;
using System.Web.Mvc;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using ScaleVoting.Extensions;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private PollDbManager PollDbManager { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController(PollValidator pollValidator)
        {
            PollDbManager = new PollDbManager();
        }

        [Authorize]
        public ActionResult Index()
        {
            Logger.Init();
            ViewBag.Polls = PollDbManager.GetPolls().Where(poll => poll.Author == UserName);

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Process(FormPoll pollForm)
        {
            Logger.Init();
            if (pollForm.Id == Guid.Empty)
            {
                return Redirect("/ControlPanel");
            }

            var poll = PollDbManager.GetPollWithId(pollForm.Id.ToString());
            if (poll.Author == UserName &&
                Enum.TryParse<PollAction>(Request.QueryString["Action"], out var realAction))
            {
                PollDbManager.ApplyPollAction(pollForm.Id.ToString(), realAction);
            }

            return Redirect("/ControlPanel");
        }
    }
}