using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScaleVoting.Domains
{
    public enum QuestionType
    {
        Single,
        Multi,
        Free
    }

    public class Question : IDistinguishable
    {
        [Key]
        public Guid Guid { get; set; }

        public string Title { get; set; }
        public QuestionType Type { get; set; }

        [Required]
        public Guid ParentGuid { get; set; }

        [ForeignKey("ParentGuid")]
        public Poll ParentPoll { get; set; }

        public int Index { get; set; }
        public List<Option> Options { get; set; }

        public Question() { }

        public Question(Poll parentPoll, int index, string title, QuestionType type,
                           IEnumerable<Option> options)
        {
            ParentPoll = parentPoll;
            ParentGuid = ParentPoll.Guid;
            Index = index;
            Guid = Guid.NewGuid();
            Title = title;
            Type = type;
            Options = new List<Option>(options);
        }
    }
}