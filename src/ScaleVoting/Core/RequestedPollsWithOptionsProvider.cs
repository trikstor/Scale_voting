using System;
using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Core
{
    public class RequestedPollsWithOptionsProvider : IRequestedPollsWithOptionsProvider
    {
        public IEnumerable<PollWithOptions> CreatePollWithOptions(PollDbContext context, string userName)
        {
            return context.Polls
                .Where(pol => pol.UserName == userName)
                .Select(poll =>
                    new PollWithOptions
                    {
                        Poll = poll,
                        Options = CreateOptions(context, poll.Id)
                    });
        }

        private IEnumerable<Option> CreateOptions(PollDbContext context, Guid pollId)
        {
            return context.Options.Where(opt => opt.PollId == pollId);
        }
    }
}