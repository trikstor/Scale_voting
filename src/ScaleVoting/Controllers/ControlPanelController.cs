using System.Web.Mvc;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private IPollProvider PollProvider { get; }

        public ControlPanelController(IPollProvider pollProvider)
        {
            PollProvider = pollProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Index(string title, string content, string[] options)
        {
            var poll = PollProvider.CreatePoll(title, content, options);
            if (poll != default(Poll))
            {
                DB.Polls.Add(poll);
                return true;
            }
            return false;
        }

        public ActionResult NewPollForm()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}