using ScaleVoting.Domains;

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
            if (!FieldValidator.FieldIsValid(poll.Title, FieldType.Title, out var detailMessage))
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
                if (question.Type == QuestionType.Single || question.Type == QuestionType.Multi)
                {
                    if (question.Options.Count < 2)
                    {
                        message = "В вопросах должно быть минимум 2 варианта ответа!";
                        return false;
                    }
                }

                if (FieldValidator.FieldIsValid(question.Title, FieldType.Title, out detailMessage))
                {
                    continue;
                }

                message = $"Заголовок '{question.Title}' {detailMessage}";
                return false;
            }
            message = "Опрос проверен успешно";

            return true;
        }
    }
}