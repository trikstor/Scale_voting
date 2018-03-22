using System.Runtime.Serialization;

namespace MyFriend.Auth
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set;}
        [DataMember]
        public string Password { get; set;}
        [DataMember]
        public string Email { get; set;}
        [DataMember]
        public int Balance { get; set; }

        public User(string name, string password, string email = "not important", int balance = 10)
        {
            Name = name;
            Password = password;
            Email = email;
            Balance = balance;
        }
    }
}