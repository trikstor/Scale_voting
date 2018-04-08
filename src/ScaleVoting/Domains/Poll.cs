using System;
using System.Web.UI.WebControls;

namespace ScaleVoting.Domains
{
    public class Poll
    {
        public string Title { get; }
        public string Content { get; }
        public string[] Options { get; }
        public Guid Id { get; }
        public PollStat Stat { get; }

        public Poll(string title, string content, string[] options)
        {
            Title = title;
            Content = content;
            Options = options;
            Id = Guid.NewGuid();
            Stat = new PollStat(Id, Options.Length);
        }
    }
}