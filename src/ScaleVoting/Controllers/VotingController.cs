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
        private PollDbContext PollDbContext { get; }
        private BCClient BCClient { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public VotingController()
        {
            PollDbContext = new PollDbContext();
            BCClient = new BCClient();
        }

        [Authorize]
        public async Task<ActionResult> Index(string id)
        {
            var poll = GetPollWithId(id);
            ViewBag.Poll = poll;
            var statisticTask = await BCClient.GetChain();
            var corretor = new BlockChainCorrector();
            question.Answers = corretor.Fix(question, statisticTask);
            ViewBag.Statistics = await GetStatistics(poll);

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(string id, FormVoting model)
        {
            var poll = GetPollWithId(id);

            var counter = 0;
            foreach (var question in poll.Questions)
            {
                var answer = new Answer(question.Id,
                    question.GetOptiionWithId(model.Answers[counter]),
                    UserName);
                await BCClient.SetAnswerToBlockChain(answer);
                counter++;
            }

            ViewBag.Poll = poll;
            var statisticTask = await BCClient.GetChain();
            var corretor = new BlockChainCorrector();
            question.Answers = corretor.Fix(question, statisticTask);
            ViewBag.Statistics = await GetStatistics(poll);

            return View(model);
        }

        private Poll GetPollWithId(string id)
        {
            return PollDbContext.Polls
                .Where(q => q.Id.ToString() == id)
                .ToList()
                .First();
        }
    }
}