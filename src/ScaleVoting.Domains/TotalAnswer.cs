using System.Collections.Generic;
using System.Linq;

namespace ScaleVoting.Domains
{
    public class TotalAnswer
    {
        public Question Question { get; }
        public IDictionary<Option, int> Statistics { get; }

        public TotalAnswer(Question question)
        {
            Question = question;
            Statistics = new Dictionary<Option, int>();

            foreach (var pollOption in Question.Options)
            {
                Statistics[pollOption] = 0;
            }

            if (Question.Answers != null && Question.Answers.Count() != 0)
            {
                foreach (var answer in Question.Answers)
                {
                    Statistics[answer.Option]++;
                }
            }
        }
    }
}
