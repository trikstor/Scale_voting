using ScaleVoting.Domains;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators
{
    public class PollValidator
    {
        private IFieldValidator FieldValidator { get; }
        public PollValidator(IFieldValidator fieldValidator)
        {
            FieldValidator = fieldValidator;
        }

        public bool PollIsValid(Poll poll, out string message)
        {
            var detailMessage = default(string);
            if (!FieldValidator.FieldIsValid(poll.Title, FieldType.Title, out detailMessage))
            {
                message = $"Заголовок '{poll.Title}' {detailMessage}";
                return false;
            }

            if (poll.Questions.Count == 0)
            {
                message = "Должен быть хотя бы один вопрос";
                return false;
            }

            foreach (var question in poll.Questions)
            {
                if (!FieldValidator.FieldIsValid(question.Title, FieldType.Title, out detailMessage))
                {
                    message = $"Заголовок '{question.Title}' {detailMessage}";
                    return false;
                }
                if (!FieldValidator.OptionsListIsValid(question.Options, out message))
                {
                    return false;
                }
            }
            message = "Опрос проверен успешно";

            return true;
        }
    }
}