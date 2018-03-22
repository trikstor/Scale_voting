namespace MyFriend.Auth
{
    public interface IFormValidator
    {
        bool FieldIsValid(string field);
        bool EmailIsValid(string email);
    }
}