using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string  JsonQuestions { get; set; }
    }
}