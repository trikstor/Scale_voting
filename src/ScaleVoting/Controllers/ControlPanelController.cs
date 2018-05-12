using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using System;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private PollDbManager PollDbManager { get; }
        private PollValidator PollValidator { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController(PollValidator pollValidator)
        {
            PollDbManager = new PollDbManager();
            PollValidator = pollValidator;
        }

        [Authorize]
        public ActionResult Index(FormPoll pollModel)
        {
            if (pollModel.Id != Guid.Empty)
            {
                PollDbManager.ApplyPollAction(pollModel.Id.ToString(), pollModel.PollAction);
            }

            ViewBag.Polls = PollDbManager.GetPolls()
                .Where(poll => poll.CreatorName == UserName);

            return View();
        }

        [Authorize]
        public ActionResult NewPollForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewPollForm(FormPoll poll)
        {
            try
            {
                var formQuestions = JsonConvert.DeserializeObject<FormQuestion[]>(poll.JsonQuestions);
                var newPoll = new Poll(UserName, poll.Title);

                newPoll.Questions = formQuestions
                    .Select((question, counter) => new Question(newPoll, counter, question.Title, question.Options))
                    .ToArray();

                string message;
                if (!PollValidator.PollIsValid(newPoll, out message))
                {
                    ModelState.AddModelError("", message);
                    return View(poll);
                }

                PollDbManager.Set(newPoll);
                ViewBag.Polls = PollDbManager.GetPolls();

                return Redirect("/ControlPanel");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}