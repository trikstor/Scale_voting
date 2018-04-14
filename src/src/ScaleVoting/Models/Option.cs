using System;
using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class Option
    {
        public Guid PollId { get; set; }
        public string PollOption { get; set; }
        [Key]
        public int Key { get; set; }
    }
}