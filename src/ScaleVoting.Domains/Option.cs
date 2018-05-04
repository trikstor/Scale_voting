using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ScaleVoting.Domains
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ParentQuestionId { get; set; }
        [JsonIgnore]
        public Question ParentQuestion { get; set; }
        public string OptionContent { get; set; }

        public Option()
        {
        }

        public Option(Question parentQuestion, string optionContent)
        {
            Id = Guid.NewGuid();
            ParentQuestion = parentQuestion;
            ParentQuestionId = ParentQuestion.Id;
            OptionContent = optionContent;
        }

        public override bool Equals(object obj)
        {
            return Equals((Option)obj);
        }

        public bool Equals(Option other)
        {
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}