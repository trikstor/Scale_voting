using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace MyFriend.Auth
{
    public class UserProvider : IUserProvider
    {
        private string FileDataPath { get; }
        private DataContractJsonSerializer JsonFormatter { get; }

        public UserProvider(string fileDataPath)
        {
            JsonFormatter = new DataContractJsonSerializer(typeof(User[]));
            FileDataPath = fileDataPath;
        }

        public Result<User> GetUser(string propertyName, string value)
        {
            User[] users;
            using (var fs = new FileStream(FileDataPath, FileMode.OpenOrCreate))
            {
                users = (User[])JsonFormatter.ReadObject(fs);
                foreach (var user in users)
                {
                    var propValue = user.GetType().GetProperty(propertyName)?
                        .GetValue(user).ToString();
                    if (propValue == value)
                        return Result.Ok(user);
                }
            }
            return Result.Fail<User>("Such a user is not found.");
        }

        public Result<User[]> GetAllUsers()
        {
            using (var fs = new FileStream(FileDataPath, FileMode.OpenOrCreate))
            {
                return Result.Ok((User[])JsonFormatter.ReadObject(fs));
            }
        }

        public Result<None> SetUser(User user)
        {
            if (GetUser("Name", user.Name).IsSuccess)
                return Result.Fail<None>("Such a user already exists.");
            if (GetUser("Email", user.Name).IsSuccess)
                return Result.Fail<None>("Such a email already exists.");
            return Result.OfAction(
                () =>
                {
                    var users = GetAllUsers();
                    using (var fs = new FileStream(FileDataPath, FileMode.Truncate))
                    {
                        var newUsers = users.Value.ToList();
                        newUsers.Add(user);
                        JsonFormatter.WriteObject(fs, newUsers.ToArray());
                    }
                });
        }
    }
}