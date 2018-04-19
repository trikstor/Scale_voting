using System;
using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Models;

namespace ScaleVoting.Core
{
    public class RequestedPollsWithOptionsProvider
    {
        public IEnumerable<PollWithOptions> CreatePollWithOptions(IEnumerable<Poll> polls, IEnumerable<Option> options, string userName)
        {
            return polls
                .Where(pol => pol.UserName == userName)
                .Select(poll =>
                    new PollWithOptions
                    {
                        Poll = poll,
                        Options = CreateOptions(options, poll.Id)
                    });
        }

        private IEnumerable<Option> CreateOptions(IEnumerable<Option> options, Guid pollId)
        {
            return options.Where(opt => opt.PollId == pollId);
        }
    }
}