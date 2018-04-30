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
        private PollDbContext PollDbContext { get; }
        private PollValidator PollValidator { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController()
        {
            PollDbContext = new PollDbContext();
            PollValidator = new PollValidator(new FieldValidator());
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Polls = PollDbContext.Polls.ToList();

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

                string message;
                if (!PollValidator.PollIsValid(newPoll, out message))
                {
                    ModelState.AddModelError("", message);
                    return View(poll);
                }

                PollDbContext.Polls.Add(newPoll);
                PollDbContext.SaveChanges();

                ViewBag.Polls = PollDbContext.Polls.ToList();

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