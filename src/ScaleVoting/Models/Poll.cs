using System;
using System.ComponentModel.DataAnnotations;
using BCClient;

namespace ScaleVoting.Models
{
    public class Poll
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
        public Question[] Questions { get; set;  }
        public BlockChainStatistics Statistics { get; set; }
        
        public Poll(User user, string title, Question[] questions)
        {
            user = User;
            Title = title;
            Questions = questions;
            Id = new Guid();
            CreationDate = DateTime.Now;
        }
    }
}