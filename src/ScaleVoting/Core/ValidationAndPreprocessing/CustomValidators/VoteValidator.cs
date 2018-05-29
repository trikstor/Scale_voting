using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Domains;
using ScaleVoting.Infrastucture;

namespace ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators
{
    public class VoteValidator
    {
        private IFieldValidator FieldValidator { get; }
        public VoteValidator(IFieldValidator fieldValidator)
        {
            FieldValidator = fieldValidator;
        }

        public static bool VoteIsValid(Vote vote, out List<string> messages)
        {
            messages = new List<string>();
            var poll = new PollDbManager().GetPollWithId(vote.PollId.ToString());
            foreach (var question in poll.Questions)
            {
                var selected = question.Options.Select(o => o.Guid).Intersect(vote.SelectedOptions).ToList();
                if (selected.Count < 1)
                {
                    messages.Add($"Не выбрана опция в вопросе {question.Title}");
                }

                if (question.Type == QuestionType.Single && selected.Count > 1)
                {
                    messages.Add($"Нельзя выбрать больше одной опции в вопросе {question.Title}");
                }
            }
            
            return messages.Count == 0;
        }
    }
}