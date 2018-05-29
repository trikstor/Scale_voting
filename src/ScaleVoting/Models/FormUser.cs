using ScaleVoting.Domains;
using ScaleVoting.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ScaleVoting.Models
{
    public class FormUser
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User ToUser(string salt) => 
            new User { UserName = Email, PasswordHash = Cryptography.Sha256(Password + salt) };
    }
}