using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormQuestion
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string[] Options { get; set; }
    }
}