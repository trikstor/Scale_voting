using System.Collections.Generic;
using System.Linq;
using BlockChainMachine.Core;
using NLog;
using ScaleVoting.Extensions;
using ScaleVoting.Models;

namespace ScaleVoting.BlockChainClient
{
    public class BlockChainStatisticsProvider
    {
        private Question Question { get; }
        private string[] AllowedOptions { get; }
        private IList<string> VotedUsersHashes { get; }
        private ILogger Logger => LogManager.GetCurrentClassLogger();
        
        public BlockChainStatisticsProvider(Question question, string[] allowedOptions)
        {
            Question = question;
            AllowedOptions = allowedOptions;
            VotedUsersHashes = new List<string>();
        }

        public BlockChainStatistics GetStatisticsFor(IEnumerable<Block> blockChain)
        {
            var result = new BlockChainStatistics(Question, AllowedOptions);

            var preBlockHash = "genesis";

            foreach (var block in blockChain)
            {
                if (preBlockHash != "genesis")
                {
                    if (Cryptography.Sha256(block.ToString()) != preBlockHash)
                    {
                        Logger.Info("Скомпрометированный блок: " +
                                          $"blockIndex: {block.Index}, questionId: {Question.Id}");
                        continue;
                    }
                }
                else
                {
                    preBlockHash = block.PreviousHash;
                }

                foreach (var transaction in block.Transactions)
                {
                    if (transaction.QuestionId != Question.Id.ToString() || 
                        (transaction.QuestionId == Question.Id.ToString() && result.UserHashes.Contains(transaction.UserHash)))
                    {
                        Logger.Info("Скомпрометированная транзакция: " +
                                          $"userHash: {transaction.UserHash}, pollId: {transaction.QuestionId}");
                        continue;
                    }
                    result.UserHashes.Add(transaction.UserHash);
                    if (AllowedOptions.Contains(transaction.OptionId))
                    {
                        result.OptionsStatistics[transaction.OptionId]++;
                    }
                    else
                    {
                        Logger.Info("Скомпрометированная транзакция: " +
                                          $"userHash: {transaction.UserHash}, pollId: {transaction.QuestionId}");
                    }
                }
            }
            return result;
        }
    }
}