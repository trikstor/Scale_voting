using System.Collections.Generic;
using System.Web.Mvc;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;
using ScaleVoting.BlockChainClient.Client;
using System.Threading.Tasks;

namespace ScaleVoting.Controllers
{
    public class VotingController : Controller
    {
        private PollDbManager PollDbManager { get; }
        private BCClient BCClient { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public VotingController()
        {
            PollDbManager = new PollDbManager();
            BCClient = new BCClient();
        }

        [Authorize]
        public async Task<ActionResult> Index(string id)
        {
            var poll = PollDbManager.GetPollWithId(id);
            ViewBag.Poll = await GetPollWithStatistics(poll);

            if (poll.IsVoted(UserName))
            {
                ViewBag.UserVoted = true;
            }
            else
            {
                ViewBag.UserVoted = false;
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(string id, FormVoting model)
        {
            var poll = PollDbManager.GetPollWithId(id);

            var counter = 0;
            foreach (var question in poll.Questions)
            {
                var answer = new Answer(question.Id,
                    question.GetOptiionWithId(model.Answers[counter]),
                    UserName);
                await BCClient.SetAnswerToBlockChain(answer);
                counter++;
            }

            ViewBag.Poll = await GetPollWithStatistics(poll);
            if(poll.IsVoted(UserName))
            {
                ViewBag.UserVoted = true;
            }
            else
            {
                ViewBag.UserVoted = false;
            }

            return View(model);
        }

        private async Task<Poll> GetPollWithStatistics(Poll poll)
        {
            var answers = await BCClient.GetChain(poll.Timestamp);

            foreach (var question in poll.Questions)
            {
                question.Answers = answers;
            }

            return poll;
        }
    }
}