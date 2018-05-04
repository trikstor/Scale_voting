using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormVoting
    {
        [Required]
        public IList<string> Answers { get; set; }

        public FormVoting()
        {
            Answers = new List<string>();
        }
    }
}