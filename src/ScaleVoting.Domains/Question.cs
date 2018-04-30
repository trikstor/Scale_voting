using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ScaleVoting.Domains
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ParentPollId { get; set; }
        [NonSerialized]
        public IEnumerable<Answer> Answers;
        [NonSerialized]
        public TotalAnswer TotalAnswer;
        public virtual Poll ParentPoll { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Option> Options { get; set; }

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