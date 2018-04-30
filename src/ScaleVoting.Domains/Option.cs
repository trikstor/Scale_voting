using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ScaleVoting.Domains
{
    public class Option : IComparable<Option>
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

        public int CompareTo(Option other)
        {
            if (Id == other.Id)
            {
                return 0;
            }
            return 1;
        }
    }
}