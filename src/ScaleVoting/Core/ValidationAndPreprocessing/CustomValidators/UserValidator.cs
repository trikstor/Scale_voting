using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScaleVoting.Domains;
using ScaleVoting.Models;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Core.ValidationAndPreprocessing.CustomValidators
{
    public class UserValidator
    {
        private IFieldValidator FieldValidator { get; }
        public UserValidator(IFieldValidator fieldValidator)
        {
            FieldValidator = fieldValidator;
        }
        public bool UserIsValid(FormUser user, out string message)
        {
            var detailMessage = default(string);
            if (!FieldValidator.FieldIsValid(user.Email, FieldType.Email, out detailMessage))
            {
                message = $"Некорректный email: {detailMessage}";
                return false;
            }
            if (!FieldValidator.FieldIsValid(user.Password, FieldType.Password, out detailMessage))
            {
                message = $"Некорректный пароль: {detailMessage}";
                return false;
            }

            message = "Опрос проверен успешно";

            return true;
        }
    }
}