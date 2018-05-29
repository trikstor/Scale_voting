using ScaleVoting.Models;

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
            if (!FieldValidator.FieldIsValid(user.Email, FieldType.Email, out var detailMessage))
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