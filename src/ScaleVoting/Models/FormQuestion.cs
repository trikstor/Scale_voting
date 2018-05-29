using System.ComponentModel.DataAnnotations;
using ScaleVoting.Domains;

namespace ScaleVoting.Models
{
    public class FormQuestion
    {
        [Required]
        public string Title { get; set; }
        public string[] Options { get; set; }
        public QuestionType Type { get; set; }
    }
}