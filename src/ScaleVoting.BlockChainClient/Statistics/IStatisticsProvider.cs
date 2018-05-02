using ScaleVoting.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Statistics
{
    public interface IStatisticsProvider
    {
        Task<Poll> GetStatistics(Poll poll);
    }
}
