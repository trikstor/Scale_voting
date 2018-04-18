using System;
using System.Collections.Generic;
using System.Linq;
using BlockChainMachine.Core;

namespace BCClient
{
    public class BlockChainStatisticsProvider
    {
        private string PollId { get; }
        private string[] AllowedOptions { get; }
        private IList<string> VotedUsersHashes { get; }

        public BlockChainStatisticsProvider(string pollId, string[] allowedOptions)
        {
            PollId = pollId;
            AllowedOptions = allowedOptions;
            VotedUsersHashes = new List<string>();
        }

        public BlockChainStatistics GetStatisticsFor(Block[] blockChain)
        {
            var result = new BlockChainStatistics(PollId, AllowedOptions);

            var preBlockHash = "genesis";

            foreach (var block in blockChain)
            {
                if (preBlockHash != "genesis")
                {
                    if (BlockChain.Hash(block) != preBlockHash)
                    {
                        Console.WriteLine($"Скомпрометированный блок: " +
                                          $"blockIndex: {block.Index}, pollId: {PollId}");
                        continue;
                    }
                }
                else
                {
                    preBlockHash = block.PreviousHash;
                }

                foreach (var transaction in block.Transactions)
                {
                    if (transaction.PollId != PollId ||
                        result.UserHashes.Contains(transaction.UserHash))
                    {
                        Console.WriteLine($"Скомпрометированная транзакция: " +
                                          $"userHash: {transaction.UserHash}, pollId: {transaction.PollId}");
                        continue;
                    }
                    result.UserHashes.Add(transaction.UserHash);
                    if (AllowedOptions.Contains(transaction.OptionId))
                    {
                        result.OptionsStatistics[transaction.OptionId]++;
                    }
                    else
                    {
                        Console.WriteLine($"Скомпрометированная транзакция: " +
                                          $"userHash: {transaction.UserHash}, pollId: {transaction.PollId}");
                    }
                }
            }
            return result;
        }
    }
}