using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ScaleVoting.BlockChainClient.BlockChainCore
{
    public static class BlockChainExtension
    {
        public static int GetBlockIndexWithTimestamp(this IEnumerable<Block> blockChain,
                                                     string timestamp)
        {
            foreach (var block in blockChain)
            {
                const string template = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
                if (DateTime.ParseExact(block.TimeStamp, template, CultureInfo.InvariantCulture) >=
                    DateTime.ParseExact(timestamp, template, CultureInfo.InvariantCulture))
                {
                    return block.Index - 1;
                }
            }

            return 1;
        }

        public static IEnumerable<Transaction> ExtractTransactions(IEnumerable<Block> blocks)
        {
            return blocks.Select(block => block.Transactions).SelectMany(trans => trans);
        }
    }
}