using System.Web.Mvc;
using ScaleVoting.Core;
using ScaleVoting.Infrastucture;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private IPollWithOptionsProvider PollWithOptionsProvider { get; }
        private string UserName => HttpContext.User.Identity.Name;
        private IRequestedPollsWithOptionsProvider RequestedPollsWithOptionsProvider =>
            new RequestedPollsWithOptionsProvider();
        public ControlPanelController(IPollWithOptionsProvider pollWithOptionsProvider)
        {
            PollWithOptionsProvider = pollWithOptionsProvider;
        }

        [Authorize]
        public ActionResult Index()
        {
            var context = new PollDbContext();
            ViewBag.Polls = RequestedPollsWithOptionsProvider.CreatePollWithOptions(context, UserName);
            context.Dispose();
            return View();
        }

        [Authorize]
        [HttpPost]
        public bool Index(string title, string content, string[] options)
        {
            var poll = PollWithOptionsProvider.CreatePoll(HttpContext.User.Identity.Name, title, content, options);
            var context = new PollDbContext();
            context.Polls.Add(poll.Poll);
            context.Options.AddRange(poll.Options);
            context.SaveChanges();

            ViewBag.Polls = RequestedPollsWithOptionsProvider.CreatePollWithOptions(context, UserName);
            context.Dispose();
            return true;
        }

        [Authorize]
        public ActionResult NewPollForm()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}