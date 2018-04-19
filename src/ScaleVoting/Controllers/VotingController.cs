using System.Linq;
using System.Web.Mvc;
using ScaleVoting.Core;
using ScaleVoting.Infrastucture;

namespace ScaleVoting.Controllers
{
    public class VotingController : Controller
    {
        private IPollDbContext PollDbContext { get; }
        private string UserName => HttpContext.User.Identity.Name;
        private RequestedPollsWithOptionsProvider RequestedPollsWithOptionsProvider =>
            new RequestedPollsWithOptionsProvider();

        public VotingController(IPollDbContext pollDbContext)
        {
            PollDbContext = pollDbContext;
        }

        [Authorize]
        public ActionResult Index(string id)
        {
            ViewBag.Poll = RequestedPollsWithOptionsProvider
                .CreatePollWithOptions(PollDbContext.Polls
                .Where(poll => poll.Id.ToString() == id), PollDbContext.Options, UserName)
                .ToArray()
                .First();

            return View();
        }
    }
}