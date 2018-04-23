using System;
using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class Option
    {   [Key]
        public Guid Key { get; set; }
        public string OptionContent { get; set; }

        public Option(string optionContent)
        {
            OptionContent = optionContent;
            Key = Guid.NewGuid();
        }
    }
}