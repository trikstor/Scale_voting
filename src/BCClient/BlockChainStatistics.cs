using System.Collections.Generic;
using ScaleVoting.Models;

namespace BCClient
{
    public class BlockChainStatistics
    {
        public Question Question { get; }
        public IList<string> UserHashes { get; }
        public IDictionary<string, int> OptionsStatistics { get; }

        public BlockChainStatistics(Question question, IEnumerable<string> pollOptions)
        {
            Question = question;
            UserHashes = new List<string>();
            OptionsStatistics = new Dictionary<string, int>();
            
            foreach (var pollOption in pollOptions)
            {
                OptionsStatistics[pollOption] = 0;
            }
        }
    }
}