using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using DCode.EmailValidator.Core;
using ScaleVoting.Domains;

namespace ScaleVoting.Core.ValidationAndPreprocessing
{
    public class FieldValidator : IFieldValidator
    {
        private static int OptionLimit =>
            int.Parse(WebConfigurationManager.AppSettings["OptionLimit"]);

        private static int TitleLimit =>
            int.Parse(WebConfigurationManager.AppSettings["TitleLimit"]);

        private static int ContentLimit =>
            int.Parse(WebConfigurationManager.AppSettings["ContentLimit"]);

        private int OptionsPerQuestionLimit =>
            int.Parse(WebConfigurationManager.AppSettings["OptionsPerQuestionLimit"]);

        private Validator EmailValidator { get; }

        public FieldValidator(Validator emailValidator)
        {
            EmailValidator = emailValidator;
        }

        public bool FieldIsValid(string field, FieldType fieldType, out string message)
        {
            message = "Успешная валидация";

            if (field == null || field.Length < 3)
            {
                message = "Длина поля не может быть меньше 3 символов";
                return false;
            }

            switch (fieldType)
            {
                case FieldType.Option when field.Length > OptionLimit:
                    message = $"Длина поля не может быть больше {OptionLimit} символов";
                    return false;
                case FieldType.Title when field.Length > TitleLimit:
                    message = $"Длина поля не может быть больше {TitleLimit} символов";
                    return false;
                case FieldType.Content when field.Length > ContentLimit:
                    message = $"Длина поля не может быть больше {ContentLimit} символов";
                    return false;
                case FieldType.Password when field.Length < 6 && field.Length > ContentLimit:
                    message = $"Длина поля не может быть больше {ContentLimit} и меньше 6 символов";
                    return false;
                case FieldType.Email when !EmailValidator.CheckDomainName(field):
                    message = "Некорректный адрес электронной почты";
                    return false;
                default:
                    return true;
            }
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