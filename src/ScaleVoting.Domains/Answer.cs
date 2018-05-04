using System;

namespace ScaleVoting.Domains
{
    public class Answer
    {
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public Option Option { get; set; }
        public Guid OptionId { get; set; }
        public User User { get; set; }
        public string UserHash { get; set; }

        public Answer()
        {      
        }

        public Answer(Question question, Option option, string user)
        {
            Question = question;
            QuestionId = Question.Id;
            Option = option;
            OptionId = option.Id;
            UserHash = user;
        }

        public Answer(Guid questionId, Option option, string user)
        {
            QuestionId = questionId;
            Option = option;
            OptionId = option.Id;
            UserHash = user;
        }
    }
}
