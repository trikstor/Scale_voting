using System;
using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class Poll
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Key]
        public Guid Id { get; set; }
    }
}