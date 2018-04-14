using System.Collections.Generic;
using ScaleVoting.Models;

namespace ScaleVoting.Core
{
    public interface IPollWithOptionsProvider
    {
        PollWithOptions CreatePoll(string userName, string title, string content, string[] options);
    }
}