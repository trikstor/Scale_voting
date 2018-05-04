using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace ScaleVoting.Domains
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ParentPollId { get; set; }
        private IEnumerable<Answer> answers;
        [JsonIgnore]
        public IEnumerable<Answer> Answers
        {
            set
            {
                answers = value;
                TotalAnswer = new TotalAnswer(this);
            }
            get => answers;
        }
        [JsonIgnore]
        public TotalAnswer TotalAnswer { get; private set; }
        public virtual Poll ParentPoll { get; set; }
        public string Title { get; set; }
        public virtual IList<Option> Options { get; set; }

        public Question()
        {
            
        }

        public Question(Poll parentPoll, string title, List<Option> options)
        {
            Id = Guid.NewGuid();
            ParentPoll = parentPoll;
            ParentPollId = ParentPoll.Id;
            Title = title;
            Options = options;
        }

        public Question(Poll parentPoll, string title, string[] options)
        {
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
    }
}