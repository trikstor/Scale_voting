using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Core.ValidationAndPreprocessing;
using ScaleVoting.Domains;
using System.Web.Configuration;
using EmailValidation;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public class FieldValidator : IFieldValidator
    {
        private int OptionLimit => int.Parse(WebConfigurationManager.AppSettings["OptionLimit"]);
        private int TitleLimit => int.Parse(WebConfigurationManager.AppSettings["TitleLimit"]);
        private int ContentLimit => int.Parse(WebConfigurationManager.AppSettings["ContentLimit"]);
        private int OptionsPerQuestionLimit => int.Parse(WebConfigurationManager.AppSettings["OptionsPerQuestionLimit"]);
        private EmailValidator EmailValidator { get; }

        public FieldValidator(EmailValidator emailValidator)
        {
            EmailValidator = emailValidator;
        }

        public bool FieldIsValid(string field, FieldType fieldType, out string message)
        {
            message = "Успешная валидация";

            if (field == null && field.Length < 3)
            {
                message = "Длина поля не может быть меньше 3 символов";
                return false;
            }
            if (fieldType == FieldType.Option && field.Length > OptionLimit)
            {
                message = $"Длина поля не может быть больше {OptionLimit} символов";
                return false;
            }
            if (fieldType == FieldType.Title && field.Length > TitleLimit)
            {
                message = $"Длина поля не может быть больше {TitleLimit} символов";
                return false;
            }
            if (fieldType == FieldType.Content && field.Length > ContentLimit)
            {
                message = $"Длина поля не может быть больше {ContentLimit} символов";
                return false;
            }
            if (fieldType == FieldType.Password && 
                field.Length < 6 && 
                field.Length > ContentLimit)
            {
                message = $"Длина поля не может быть больше {ContentLimit} и меньше 6 символов";
                return false;
            }
            if (fieldType == FieldType.Email)
            {
                if (!EmailValidator.Validate(field, out var emailValidationResult))
                {
                    message = emailValidationResult.ToString();
                    // !!!
                    return true;
                }
            }

            return true;
        }

        public bool OptionsListIsValid(IEnumerable<Option> options, out string message)
        {
            var optionsList = options.ToList();
            message = "успешная валидация";

            if (optionsList.Count < 2)
            {
                message = "вариантов ответов должно быть больше 2";
                return false;
            }
            if (optionsList.Count > OptionsPerQuestionLimit)
            {
                message = $"вариантов ответов должно быть меньше {OptionsPerQuestionLimit}";
                return false;
            }
            foreach (var option in optionsList)
            {
                if (!FieldIsValid(option.OptionContent, FieldType.Option, out message))
                {
                    return false;
                }
            }
            return true;
        }
    }
}