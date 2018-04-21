using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ScaleVoting.Core;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private IQuestionProvider QuestionProvider { get; }
        private IPollDbContext PollDbContext { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController(IQuestionProvider questionProvider, IPollDbContext pollDbContext)
        {
            QuestionProvider = questionProvider;
            PollDbContext = pollDbContext;
        }

        [Authorize]
        public ActionResult Index()
        {
            var questions = PollDbContext.Questions
                .Where(question => question.UserName == UserName);
            
            foreach (var question in questions)
            {
                question.SetOptionsFromContext(PollDbContext.Options);
            }

            ViewBag.Questions = questions;
            //context.Dispose();
            return View();
        }

        [Authorize]
        [HttpPost]
        public bool Index(string title, string content, string[] options)
        {
            var currentQuestion = QuestionProvider.CreatePoll(HttpContext.User.Identity.Name, title, content, options);
            var context = new PollDbContext();
            context.Questions.Add(currentQuestion);
            context.Options.AddRange(currentQuestion.Options);
            context.SaveChanges();

            var questions = PollDbContext.Questions
                .Where(question => question.UserName == UserName);
            
            foreach (var question in questions)
            {
                question.SetOptionsFromContext(PollDbContext.Options);
            }

            ViewBag.Questions = questions;
                
            //context.Dispose();
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