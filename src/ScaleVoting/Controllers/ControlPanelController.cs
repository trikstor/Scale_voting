using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using System;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private PollDbManager PollDbManager { get; }
        private PollValidator PollValidator { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController()
        {
            PollDbManager = new PollDbManager();
            PollValidator = new PollValidator(new FieldValidator());
        }

        [Authorize]
        public ActionResult Index()
        {
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
                    .Select(question => new Question(newPoll, question.Title, question.Options))
                    .ToArray();

                if (!PollValidator.PollIsValid(newPoll, out var message))
                {
                    ModelState.AddModelError("", message);
                    return View(poll);
                }

                PollDbManager.SetPoll(newPoll);
                ViewBag.Polls = PollDbManager.GetPolls();

                return Redirect("/ControlPanel");
            }
            catch (Exception e)
            {
                //TODO: Логгирование
                return View();
            }
        }
    }
}