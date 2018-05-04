using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class PollDbManager
    {
        private PollDbContext PollDbContext { get; }

        public PollDbManager()
        {
            PollDbContext = new PollDbContext();
        }

        public Poll GetPollWithId(string id)
        {
            return PollDbContext.Polls
                .Where(q => q.Id.ToString() == id)
                .ToList()
                .First();
        }

        public List<Poll> GetPolls() => PollDbContext.Polls.ToList();

        public void SetPoll(Poll poll)
        {
            PollDbContext.Polls.Add(poll);
            PollDbContext.SaveChanges();
        }
    }
}
