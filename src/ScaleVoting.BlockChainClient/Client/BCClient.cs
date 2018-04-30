﻿using Newtonsoft.Json;
using ScaleVoting.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Client
{
    public class BCClient
    {
        private WebClient Client { get; }
        private IBlockChainCorrector BlockChainCorrector { get; }
        public BCClient()
        {
            Client = new WebClient
            {
                AllNodeUri = NodeReplics.NodeReplicsAddresses,
                TimeoutInMsec = 2000
            };

            BlockChainCorrector = new BlockChainCorrector();
        }

        public async Task<IEnumerable<Answer>> GetChain()
        {
            var blocks = await GetChainBlocks();
            blocks = BlockChainCorrector.Fix(blocks.ToArray());
            var transactions = BlockChainExtension.ConvertBlocksToTransactions(blocks);
            var answers = transactions.Select(trans => JsonConvert.DeserializeObject<Answer>(trans.Data));

            return answers;
        }

        private async Task<IEnumerable<Block>> GetChainBlocks()
        {
            var jsonChain = await Client.GetResponseFromRequestTo(NodeApi.Chain);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
            settings.Converters.Add(new TransactionConverter());
            var blocks = JsonConvert.DeserializeObject<Block[]>(jsonChain, settings);

            return blocks;
        }

        public async Task<string> SetAnswerToBlockChain(Answer answer)
        {
            var transaction = new Transaction
            {
                Data = JsonConvert.SerializeObject(answer),
                HasValidData = true,
                UserHash = answer.UserHash,
                Signature = new byte[10]
            };
            var jsonTransaction = JsonConvert.SerializeObject(transaction,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            return await Client.GetResponseFromRequestWithJsonTo(
                NodeApi.NewTransaction, jsonTransaction);
        }
    }
}