using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        
        [Required]
        public string[] Options { get; set; }
    }
}