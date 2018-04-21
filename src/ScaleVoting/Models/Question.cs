using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScaleVoting.Models
{
    public class Question
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Key]
        public Guid Id { get; set; }
        
        public IEnumerable<Option> Options { get; set; }

        public void SetOptionsFromContext(IEnumerable<Option> options)
        {
            Options = options.Where(opt => opt.PollId == Id);
        }
    }
}