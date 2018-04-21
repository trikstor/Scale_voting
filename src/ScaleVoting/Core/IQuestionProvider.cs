using System.Collections.Generic;
using ScaleVoting.Models;

namespace ScaleVoting.Core
{
    public interface IQuestionProvider
    {
        Question CreatePoll(string userName, string title, string content, IEnumerable<string> options);
    }
}