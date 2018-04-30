using ScaleVoting.BlockChainClient.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScaleVoting.BlockChainClient
{
    public static class BlockChainExtension
    {
        public static int GetBlockIndexWithTimestamp(this Block[] blockChain, string timestamp)
        {
            const int step = 3;

            for (var counter = 0; counter < blockChain.Length; counter += step)
            {
                if (TimeSpan.Parse(blockChain[counter].TimeStamp) < TimeSpan.Parse(timestamp))
                {
                    continue;
                }

                var limit = counter >= step ? counter - step : 0;
                while (counter > limit)
                {
                    if (TimeSpan.Parse(blockChain[counter].TimeStamp) <= TimeSpan.Parse(timestamp))
                    {
                        return counter;
                    }
                    counter--;
                }
            }
            return -1;
        }

        public static IEnumerable<ITransaction> ConvertBlocksToTransactions(IEnumerable<Block> blocks)
        {
            return blocks
                .Select(block => block.Transactions)
                .SelectMany(trans => trans);
        }
    }
}