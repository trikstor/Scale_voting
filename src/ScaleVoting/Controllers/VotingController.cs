using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;
using ScaleVoting.BlockChainClient.Client;
using System.Threading.Tasks;
using ScaleVoting.BlockChainClient.BlockChainCorrectors;

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

            return View(model);
        }

        private async Task<Poll> GetPollWithStatistics(Poll poll)
        {
            var statisticTask = await BCClient.GetChain(poll.Timestamp);
            var tt = statisticTask.ToList();
            var corretor = new BlockChainCorrector();
            for(var counter = 0; counter < poll.Questions.Count; counter++)
            {
                poll.Questions[counter].Answers = corretor.Fix(poll.Questions[counter], statisticTask);
            }

            return poll;
        }
    }
}