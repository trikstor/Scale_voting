using System;
using System.Text;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public class Shielding : IPreprocessing
    {
        public string Process(string content)
        {
            var sb = new StringBuilder();
            
            foreach (var currentChar in content)
            {
                sb.Append(currentChar);
            }
            return sb.ToString();
        }
    }
}