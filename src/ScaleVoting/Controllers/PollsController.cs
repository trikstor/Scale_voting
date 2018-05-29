using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ScaleVoting.BlockChainClient.Client;
using ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators;
using ScaleVoting.Domains;
using ScaleVoting.Extensions;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class PollsController : Controller
    {
        private PollDbManager PollDbManager { get; }
        private PollValidator PollValidator { get; }
        private BcClient BcClient { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public PollsController(PollValidator pollValidator)
        {
            PollDbManager = new PollDbManager();
            PollValidator = pollValidator;
            BcClient = new BcClient();
        }

        [Authorize]
        public async Task<ActionResult> Vote(string id)
        {
            Logger.Init();
            try
            {
                var poll = PollDbManager.GetPollWithId(id);

                // TODO Switch to server votecheck logic
                var votes = await BcClient.GetVotesFromDate(poll.TimeStamp);
                poll.Votes = votes;
                ViewBag.Poll = poll;

                ViewBag.UserVoted = poll.HasVoted(UserName);

                return View();
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Vote(FormVote formVote)
        {
            Logger.Init();
            try
            {
                var vote = formVote.ToVote(UserName);
                if (!VoteValidator.VoteIsValid(vote, out var messages))
                {
                    var poll = PollDbManager.GetPollWithId(vote.PollId.ToString());
                    ViewBag.Poll = poll;
                    ViewBag.UserVoted = poll.HasVoted(UserName);
                    ViewBag.Errors = messages;
                    return View(formVote);
                }
                await BcClient.SendVoteToBlockChain(vote);

                return Redirect("/");
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
        }


        [Authorize]
        public async Task<ActionResult> Stat(string id)
        {
            Logger.Init();
            try
            {
                var poll = PollDbManager.GetPollWithId(id);
                var votes = await BcClient.GetVotesFromDate(poll.TimeStamp);
                poll.Votes = votes;
                var stat = new Statistics(poll);
                ViewBag.stat = stat;

                return View();
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(FormPoll model)
        {
            Logger.Init();

            try
            {
                var poll = model.ToPoll(UserName);

                if (!PollValidator.PollIsValid(poll, out var message))
                {
                    ModelState.AddModelError("", message);
                    return View(model);
                }

                PollDbManager.Set(poll);
                ViewBag.Polls = PollDbManager.GetPolls();

                return Redirect("/ControlPanel");
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
                return Redirect("/ControlPanel");
            }
        }
    }
}