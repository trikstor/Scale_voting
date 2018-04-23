using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScaleVoting.Domains
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<Option> Options { get; set; }

        public Question(string content, IEnumerable<Option> options)
        {
            Guid.NewGuid();
            Content = content;
            Options = options;
        }
    }
}