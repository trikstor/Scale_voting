using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ScaleVoting.Extensions;

namespace ScaleVoting.Domains
{
    public class Poll : IDistinguishable
    {
        [Key]
        public Guid Guid { get; set; }

        public string Title { get; set; }
        public string TimeStamp { get; set; }
        public List<Question> Questions { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public string Author { get; set; }
        public bool IsClosed { get; set; }

        public Poll() { }

        public Poll(string username, string title)
        {
            Author = username;
            Title = title;
            Guid = Guid.NewGuid();
            TimeStamp = Extensions.TimeStamp.Get(DateTime.Now);
        }

        public bool HasVoted(string username)
        {
            return Votes != null && Votes.Any(x => x.UserHash == Cryptography.Sha256(username));
        }
    }
}