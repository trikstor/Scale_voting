namespace MyFriend.Auth
{
    public interface IUserProvider
    {
        Result<User> GetUser(string propertyName, string name);
        Result<None> SetUser(User user);
    }
}