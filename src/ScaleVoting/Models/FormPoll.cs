using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public Poll[] Question { get; set; }
    }
}