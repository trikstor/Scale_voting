using Newtonsoft.Json;
using ScaleVoting.Domains;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Client
{
    public class BlockChainClient
    {
        private WebClient Client { get; }
        private IBlockChainCorrector BlockChainCorrector { get; }
        public BlockChainClient(IBlockChainCorrector blockChainCorrector)
        {
            BlockChainCorrector = blockChainCorrector;
            Client = new WebClient
            {
                AllNodeUri = NodeReplics.NodeReplicsAddresses.ToArray(),
                TimeoutInMsec = int.Parse(ConfigurationManager.AppSettings["ReplicWaitingMsec"])
            };
        }

        public async Task<IEnumerable<Answer>> GetChain()
        {
            var blocks = await GetChainBlocks();
            var transactions = blocks
                .Select(block => block.Transactions)
                .SelectMany(trans => trans);
            var answers = transactions.Select(trans => JsonConvert.DeserializeObject<Answer>(trans.Data));

            return BlockChainCorrector.Fix(answers);
        }

        private async Task<IEnumerable<Block>> GetChainBlocks()
        {
            var jsonChain = await Client.GetResponseFromRequestTo(NodeApi.Chain);
            var blocks = JsonConvert.DeserializeObject<Block[]>(jsonChain);

            return BlockChainCorrector.Fix(blocks);
        }

        public async Task<string> SetAnswerToBlockChain(Answer answer)
        {
            var transaction = new Transaction
            {
                Data = JsonConvert.SerializeObject(answer),
                HasValidData = true,
                UserHash = "",
                Signature = new byte[10]
            };
            var jsonTransaction = JsonConvert.SerializeObject(transaction);

            return await Client.GetResponseFromRequestWithJsonTo(
                NodeApi.NewTransaction, jsonTransaction);
        }
    }
}
