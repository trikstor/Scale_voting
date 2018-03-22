namespace MyFriend.Auth
{
    public class Sign
    {
        private IFormValidator Validator { get; }
        private IUserProvider UserProvider { get; }
        
        public Sign(IFormValidator formValidator, IUserProvider userProvider)
        {
            Validator = formValidator;
            UserProvider = userProvider;
        }
        
        public Result<None> In(User actualUser)
        {
            actualUser = actualUser.ToLower();
            if (!Validator.FieldIsValid(actualUser.Name)
                || !Validator.FieldIsValid(actualUser.Password))
            {
                return Result.Fail<None>("Неверные данные.");
            }

            var expectedUser = UserProvider.GetUser("Name", actualUser.Name);
            if (!expectedUser.IsSuccess)
                return Result.Fail<None>(expectedUser.Error);
            if (expectedUser.Value.Password != actualUser.Password)
                return Result.Fail<None>("Неверный пароль.");

            return Result.Ok();
        }

        public Result<None> Up(User user)
        {
            user = user.ToLower();
            if(!Validator.FieldIsValid(user.Name) 
               || !Validator.FieldIsValid(user.Password) 
               || !Validator.EmailIsValid(user.Email))
            {
                return Result.Fail<None>("Неверные данные.");
            }
            return UserProvider.SetUser(user);
        }
    }
}