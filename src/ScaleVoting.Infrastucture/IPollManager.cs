using ScaleVoting.Domains;
using System.Collections.Generic;

namespace ScaleVoting.Infrastucture
{
    public interface IPollManager
    {
        Poll GetPollWithId(string id);
        List<Poll> GetPolls();
        void SetPoll(Poll poll);
    }
}
