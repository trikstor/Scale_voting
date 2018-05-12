using System;
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
            var poll = PollDbContext.Polls
                .Where(q => q.Id.ToString() == id)
                .ToList()
                .First();
            poll.Questions = poll.Questions.OrderBy(q => q.Index).ToList();

            return poll;
        }

        public List<Poll> GetPolls()
        {
            var polls = PollDbContext.Polls
                .OrderByDescending(q => q.Timestamp)
                .ToList();

            for(var counter = 0; counter < polls.Count; counter++)
            {
                polls[counter].Questions = polls[counter].Questions
                    .OrderBy(q => q.Index)
                    .ToList();
            }

            return polls;
        }

        public void Set(Poll poll)
        {
            PollDbContext.Polls.Add(poll);
            PollDbContext.SaveChanges();
        }

        public void Delete(Poll poll)
        {
            PollDbContext.Polls.Remove(poll);
        }

        public void ApplyPollAction(string id, PollAction pollAction)
        {
            var closablePoll = GetPollWithId(id);
            switch (pollAction)
            {
                case PollAction.Close:
                    closablePoll.IsClosed = true;
                    break;
                case PollAction.Open:
                    closablePoll.IsClosed = false;
                    break;
                case PollAction.Delete:
                    Delete(closablePoll);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            PollDbContext.SaveChanges();
        }
    }
}
