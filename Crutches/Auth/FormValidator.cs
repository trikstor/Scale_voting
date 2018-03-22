using EmailValidation;

namespace MyFriend.Auth
{
    public class FormValidator : IFormValidator
    {       
        public bool FieldIsValid(string field)
        {
            var specialCharacters = new[]
            {
                '\"',
                '\'',
                '\\',
                '{',
                '}',
                ' '
            };
            return field != null && field.Length > 2 && field.Length < 15 && field.NotContains(specialCharacters);
        }

        public bool EmailIsValid(string email)
        {
            return EmailValidator.Validate(email);
        }
    }
}