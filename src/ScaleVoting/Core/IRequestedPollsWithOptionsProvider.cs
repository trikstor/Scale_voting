using System.Collections.Generic;
using ScaleVoting.Infrastucture;
using ScaleVoting.Models;

namespace ScaleVoting.Core
{
    interface IRequestedPollsWithOptionsProvider
    {
        IEnumerable<PollWithOptions> CreatePollWithOptions(PollDbContext context, string userName);
    }
}
