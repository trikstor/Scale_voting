using ScaleVoting.Extensions;
using System;

namespace ScaleVoting.Domains
{
    public class Answer
    {
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public Option Option { get; set; }
        public Guid OptionId { get; set; }
        public string UserHash { get; set; }

        public Answer()
        {      
        }

        public Answer(Question question, Option option, string userName)
        {
            Question = question;
            QuestionId = Question.Id;
            Option = option;
            OptionId = option.Id;
            UserHash = Cryptography.Sha256(userName);
        }

        public Answer(Guid questionId, Option option, string userName)
        {
            QuestionId = questionId;
            Option = option;
            OptionId = option.Id;
            UserHash = Cryptography.Sha256(userName);
        }
    }
}
