using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var poll = PollDbContext.Polls.Where(q => q.Guid.ToString() == id).
                Include(p => p.Questions.Select(q => q.Options)).ToList().First();
            poll.Questions = poll.Questions.OrderBy(q => q.Index).ToList();

            return poll;
        }

        public Option GetOptionWithGuid(Guid guid)
        {
            var option = PollDbContext.Options.Where(o => o.Guid == guid).ToList().First();

            return option;
        }

        public List<Poll> GetPolls()
        {
            var polls = PollDbContext.Polls.Include(p => p.Questions.Select(q => q.Options)).
                OrderByDescending(p => p.TimeStamp).ToList();
            ;

            foreach (var poll in polls)
            {
                poll.Questions = poll.Questions.OrderByDescending(q => q.Index).ToList();
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