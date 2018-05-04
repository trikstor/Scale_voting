using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScaleVoting.Extensions;

namespace ScaleVoting.Domains
{
    public class Poll
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public string Timestamp { get; set; }
        public virtual ICollection<Question> Questions { get; set;  }

        public Poll()
        {
            
        }
        public Poll(string userName, string title)
        {
            CreatorName = userName;
            Title = title;
            Id = Guid.NewGuid();
            Timestamp = TimeStamp.Get(DateTime.Now);
        }

        public Poll(string userName, string title, Question[] questions)
        {
            CreatorName = userName;
            Title = title;
            Questions = questions;
            Id = Guid.NewGuid();
            Timestamp = TimeStamp.Get(DateTime.Now);
        }
    }
}