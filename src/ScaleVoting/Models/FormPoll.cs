using System;
using System.ComponentModel.DataAnnotations;
using ScaleVoting.Infrastucture;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string  JsonQuestions { get; set; }
        public PollAction PollAction { get; set; }
    }
}