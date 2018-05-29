using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ScaleVoting.Extensions
{
    public class Cryptography
    {
        public static string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            var hash = string.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            return crypto.Aggregate(hash, (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}