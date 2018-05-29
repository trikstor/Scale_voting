using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScaleVoting.Domains
{
    public class Option : IDistinguishable
    {
        [Key]
        public Guid Guid { get; set; }

        public string Title { get; set; }

        [Required]
        public Guid ParentGuid { get; set; }

        [ForeignKey("ParentGuid")]
        public Question ParentQuestion { get; set; }

        public string OptionContent { get; set; }

        public Option(string optionContent)
        {
            ParentGuid = Guid.Empty;
            Guid = Guid.NewGuid();
            OptionContent = optionContent;
        }

        public Option() { }

        public Option(string optionContent, Question parentQuestion) : this(optionContent)
        {
            ParentQuestion = parentQuestion;
            ParentGuid = ParentQuestion.Guid;
        }

        public override bool Equals(object obj)
        {
            return EqualId((Option) obj);
        }

        private bool EqualId(IDistinguishable other)
        {
            return other.Guid == Guid;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}