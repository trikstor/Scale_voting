using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ScaleVoting.Domains;
using ScaleVoting.Models;

namespace ScaleVoting.BlockChainClient
{
    public class BlockChainStatistics
    {
        private Question question;
        private string[] allowedOptions;

        [ForeignKey("Question")]
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

        public BlockChainStatistics(Question question, string[] allowedOptions)
        {
            this.question = question;
            this.allowedOptions = allowedOptions;
        }
    }
}