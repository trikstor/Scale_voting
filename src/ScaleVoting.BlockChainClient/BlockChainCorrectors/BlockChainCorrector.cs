using System.Collections.Generic;
using ScaleVoting.BlockChainClient.BlockChainCore;

namespace ScaleVoting.BlockChainClient.BlockChainCorrectors
{
    public class BlockChainCorrector : IBlockChainCorrector
    {
        public IEnumerable<Block> Fix(Block[] chain, string startTimeStamp)
        {
            var result = new List<Block>();

            var startIndex = chain.GetBlockIndexWithTimestamp(startTimeStamp);

            for (var counter = startIndex; counter < chain.Length; counter++)
            {
                var admittedTransactions = new List<Transaction>();
                foreach (var transaction in chain[counter].Transactions)
                {
                    if (!transaction.Valid)
                    {
                        continue;
                    }

                    admittedTransactions.Add(transaction);
                }

                chain[counter].Transactions = admittedTransactions;
                result.Add(chain[counter]);
            }

            return result;
        }
    }
}