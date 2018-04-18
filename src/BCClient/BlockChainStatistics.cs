using System.Collections.Generic;

namespace BCClient
{
    public class BlockChainStatistics
    {
        public string PollId { get; }
        public IList<string> UserHashes { get; set; }
        public IDictionary<string, int> OptionsStatistics { get; set; }

        public BlockChainStatistics(string pollId, string[] pollOptions)
        {
            PollId = pollId;
            UserHashes = new List<string>();
            OptionsStatistics = new Dictionary<string, int>();
            
            foreach (var pollOption in pollOptions)
            {
                OptionsStatistics[pollOption] = 0;
            }
        }
    }
}