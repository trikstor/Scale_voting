using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Controllers
{
    public class ControlPanelController : Controller
    {
        private IPollDbContext PollDbContext { get; }
        private string UserName => HttpContext.User.Identity.Name;

        public ControlPanelController(IPollDbContext pollDbContext)
        {
            PollDbContext = pollDbContext;
        }

        [Authorize]
        public ActionResult Index()
        {
            /*
            var questions = PollDbContext.Questions
                .Where(question => question.UserName == UserName);
            
            foreach (var question in questions)
            {
                question.SetOptionsFromContext(PollDbContext.Options);
            }

            ViewBag.Questions = questions;
            //context.Dispose();
            */
            return View();
        }

        [Authorize]
        [HttpPost]
        public bool Index(
            string title, string content, string[] questions, int[] optionPerQuestions, string[] options)
        {
            var processedOptions = options.Select(opt => new Option(opt)).ToArray();
            var processedQuestions = new List<Question>();
            var currentOptionPos = 0;
            
            for (var counter = 0; counter < questions.Length; counter++)
            {
                var optionForQuestion = new List<Option>();
                
                var optionsCounter = optionPerQuestions[counter];
                while (optionsCounter > 0)
                {
                    optionForQuestion.Add(processedOptions[currentOptionPos]);
                    currentOptionPos++;
                    optionsCounter--;
                }
                
                processedQuestions.Add(new Question(questions[counter], optionForQuestion));
            }
            /*
            var context = new PollDbContext();
            context.Questions.Add(currentQuestion);
            context.Options.AddRange(currentQuestion.Options);
            context.SaveChanges();

            var questions1 = PollDbContext.Questions
                .Where(question => question.UserName == UserName);
            
            foreach (var question in questions)
            {
                question.SetOptionsFromContext(PollDbContext.Options);
            }

            ViewBag.Questions = questions;
                
            //context.Dispose();
            */
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