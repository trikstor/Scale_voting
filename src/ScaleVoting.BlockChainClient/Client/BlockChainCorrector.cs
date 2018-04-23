using ScaleVoting.Domains;
using ScaleVoting.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ScaleVoting.BlockChainClient.Client
{
    public class BlockChainCorrector : IBlockChainCorrector
    {
        public Question Question { get; }
        public string[] AllowedOptions { get; }
        private IList<string> VotedUsersHashes { get; }

        public BlockChainCorrector(Question question, string[] allowedOptions)
        {
            Question = question;
            AllowedOptions = allowedOptions;
            VotedUsersHashes = new List<string>();
        }

        public IEnumerable<Block> Fix(IEnumerable<Block> blockChain)
        {
            var result = new List<Block>();

            var preBlockHash = "genesis";

            foreach (var block in blockChain)
            {
                if (preBlockHash != "genesis")
                {
                    if (Cryptography.Sha256(block.ToString()) != preBlockHash)
                    {
                        continue;
                    }
                }
                else
                {
                    preBlockHash = block.PreviousHash;
                }

                foreach (var transaction in block.Transactions)
                {
                    if (VotedUsersHashes.Contains(transaction.UserHash))
                    {
                        continue;
                    }
                    VotedUsersHashes.Add(transaction.UserHash);
                }
                result.Add(block);
            }
            return result;
        }

        public IEnumerable<Answer> Fix(IEnumerable<Answer> answers)
        {
            var result = new List<Answer>();

            foreach(var answer in answers)
            {
                
            }

            return result;
        }
    }
}
