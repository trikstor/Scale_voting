﻿using ScaleVoting.Domains;
using ScaleVoting.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ScaleVoting.BlockChainClient.BlockChainCorrectors
{
    public class BlockChainCorrector : IBlockChainCorrector
    {
        private IList<string> VotedUsersHashes { get; }

        public BlockChainCorrector()
        {
            VotedUsersHashes = new List<string>();
        }

        public IEnumerable<Block> Fix(Block[] blockChain, string startTimestamp)
        {
            var result = new List<Block>();
            var preBlock = default(Block);
            // Можно будет доделать после правок в блокчейне, связанных с форматом time stamp.
            var startIndex = 0;//blockChain.GetBlockIndexWithTimestamp(startTimestamp);

            for(var counter = startIndex; counter < blockChain.Length; counter++)
            {
                if (preBlock != default(Block) && preBlock.PreviousHash != "genesis")
                {
                    if (Cryptography.Sha256(preBlock.ToString()) != blockChain[counter].PreviousHash)
                    {
                        preBlock = blockChain[counter];
                        continue;
                    }
                }
                else
                {
                    preBlock = blockChain[counter];
                    continue;
                }
                preBlock = blockChain[counter];

                foreach (var transaction in blockChain[counter].Transactions)
                {
                    /*
                    if (VotedUsersHashes.Contains(transaction.UserHash))
                    {
                        continue;
                    }
                    VotedUsersHashes.Add(transaction.UserHash);
                    */
                }
                result.Add(blockChain[counter]);
            }
            return result;
        }
    }
}
