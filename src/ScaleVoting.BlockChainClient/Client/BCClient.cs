using Newtonsoft.Json;
using ScaleVoting.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScaleVoting.BlockChainClient.Transaction;
using ScaleVoting.BlockChainClient.BlockChainCorrectors;

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

        public async Task<IEnumerable<Answer>> GetChain(string startTimestamp)
        {
            var blocks = await GetChainBlocks();
            blocks = BlockChainCorrector.Fix(blocks.ToArray(), startTimestamp);
            var transactions = BlockChainExtension.ConvertBlocksToTransactions(blocks);
            var answers = new List<Answer>();
            foreach (var transaction in transactions)
            {
                answers.Add(transaction.ToAnswer());
            }

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
            var jsonTransaction = JsonConvert.SerializeObject(answer.ToTransaction(),
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            return await Client.GetResponseFromRequestWithJsonTo(
                NodeApi.NewTransaction, jsonTransaction);
        }
    }
}
