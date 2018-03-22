namespace MyFriend.Auth
{
    public static class UserExtension
    {
        public static User ToLower(this User user)
        {
            user.Name = user.Name.ToLower();
            user.Email = user.Email.ToLower();
            return user;
        }
    }
}