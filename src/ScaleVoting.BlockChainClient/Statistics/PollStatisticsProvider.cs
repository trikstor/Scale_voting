using ScaleVoting.Domains;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Statistics
{
    public class PollStatisticsProvider : IStatisticsProvider
    {
        private async Task<Poll> GetStatistics(Poll poll)
        {
            foreach (var question in poll.Questions)
            {
                question.TotalAnswer = new TotalAnswer(question);
            }
            return poll;
        }
    }
}
