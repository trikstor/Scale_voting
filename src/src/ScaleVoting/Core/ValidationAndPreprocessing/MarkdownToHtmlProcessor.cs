using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public class MarkdownToHtmlProcessor// : IPreprocessing
    {
        private readonly char[] AllowedChars = new[] {'*'};
        
        public string Process(string content)
        {
            var sb = new StringBuilder();
            
            for(var charPos = 0; charPos < content.Length; charPos++)
            {
                if (content[charPos] == '#')
                {
                    var headerLevel = 1;
                    while (content[charPos] == '#')
                    {
                        headerLevel++;
                        charPos++;
                    }

                    sb.Append($"<h{headerLevel}>");
                    while (content[charPos] != '\n')
                    {
                        sb.Append(content[charPos]);
                        charPos++;
                    }
                    sb.Append($"</h{headerLevel}>");
                    sb.Append('\n');
                }
                else
                {
                    sb.Append(content[charPos]);
                }
            }
            return sb.ToString();
        }
    }
}