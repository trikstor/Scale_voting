using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using ScaleVoting.Domains;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string JsonQuestions { get; set; }

        public Poll ToPoll(string userName)
        {
            var formQuestions = JsonConvert.DeserializeObject<FormQuestion[]>(JsonQuestions);
            var newPoll = new Poll(userName, Title) {Questions = new List<Question>()};
            var counter = 0;

            foreach (var formQuestion in formQuestions)
            {
                var options = formQuestion.Options.Select(x => new Option(x)).ToArray();
                newPoll.Questions.Add(new Question(newPoll, counter, formQuestion.Title,
                                                   formQuestion.Type, options));

                foreach (var option in newPoll.Questions.Last().Options)
                {
                    option.ParentQuestion = newPoll.Questions.Last();
                }

                counter++;
            }

            return newPoll;
        }
    }
}