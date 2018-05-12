using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using ScaleVoting.Extensions;

namespace ScaleVoting.Domains
{
    public class Question
    {
        public int Index { get; set; }
        [Key]
        public Guid Id { get; set; }
        [JsonIgnore]
        public TotalAnswer TotalAnswer { get; private set; }
        public Guid ParentPollId { get; set; }
        public virtual Poll ParentPoll { get; set; }
        public string Title { get; set; }
        public virtual IList<Option> Options { get; set; }
        private IEnumerable<Answer> answers;
        [JsonIgnore]
        public IEnumerable<Answer> Answers
        {
            set
            {
                answers = Fix(value);
                TotalAnswer = new TotalAnswer(this);
            }
            get
            {
                return answers;
            }
        }

        public Question()
        {
        }

        public Question(Poll parentPoll, int index, string title, List<Option> options)
        {
            Index = index;
            Id = Guid.NewGuid();
            ParentPoll = parentPoll;
            ParentPollId = ParentPoll.Id;
            Title = title;
            Options = options;
        }

        public Question(Poll parentPoll, int index, string title, string[] options)
        {
            Index = index;
            Id = Guid.NewGuid();
            ParentPoll = parentPoll;
            ParentPollId = ParentPoll.Id;
            Title = title;
            Options = new List<Option>();
            foreach (var option in options)
            {
                Options.Add(new Option(this, option));
            }
        }

        public Option GetOptiionWithId(string id) => Options.First(opt => opt.Id.ToString() == id);

        public IEnumerable<Answer> Fix(IEnumerable<Answer> answers)
        {
            var resultAnswers = new List<Answer>();

            foreach (var answer in answers)
            {
                if (Options.All(opt => opt.Id != answer.OptionId) ||
                    Id != answer.QuestionId)
                {
                    continue;
                }
                resultAnswers.Add(answer);
            }

            return resultAnswers;
        }

        public bool IsAnswerWithUser(string userName)
        {
            var userHash = Cryptography.Sha256(userName);
            var tt = Answers.ToArray();
            foreach(var answer in Answers)
            {
                if(answer.UserHash == userHash)
                {
                    return true;
                }
            }
            return false;
        }
    }
}