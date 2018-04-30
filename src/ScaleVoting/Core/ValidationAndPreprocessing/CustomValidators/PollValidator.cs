using ScaleVoting.Domains;
using ScaleVoting.Models.ValidationAndPreprocessing;
using System.Linq;

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
            if (!FieldValidator.FieldIsValid(poll.Title, FieldType.Title))
            {
                message = $"Заголовок '{poll.Title}' должен быть меньше 150 символов.";
                return false;
            }

            foreach (var question in poll.Questions)
            {
                if (!FieldValidator.FieldIsValid(question.Title, FieldType.Title))
                {
                    message = $"Заголовок '{question.Title}' должен быть меньше 150 символов.";
                    return false;
                }
                if (!FieldValidator.OptionsListIsValid(question.Options.Select(opt => opt.OptionContent).ToList()))
                {
                    message = "Вариантов ответа должно быть не более 50 и не менее 2, " +
                        "недопустимо повторение вариантов ответа в одном вопросе";
                    return false;
                }
            }
            message = "Опрос проверен успешно";
            return true;
        }
    }
}