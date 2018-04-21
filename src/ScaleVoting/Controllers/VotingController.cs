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

        public VotingController(IPollDbContext pollDbContext)
        {
            PollDbContext = pollDbContext;
        }

        [Authorize]
        public ActionResult Index(string id)
        {
            var question = PollDbContext.Questions
                .Where(q => q.Id.ToString() == id)
                .ToArray()
                .First();
            
            question.SetOptionsFromContext(PollDbContext.Options);

            ViewBag.Question = question;

            return View();
        }
    }
}