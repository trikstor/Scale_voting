using System.Collections.Generic;
using System.Linq;

namespace MyFriend.Auth
{
    public static class ValidatorExtension
    {
        public static bool NotContains(this string str, IEnumerable<char> characters)
        {
            return characters
                .All(character => !str.Contains(character.ToString()));
        }
    }
}