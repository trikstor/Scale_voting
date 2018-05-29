using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScaleVoting.BlockChainClient.BlockChainCore;
using ScaleVoting.BlockChainClient.BlockChainCorrectors;
using ScaleVoting.Domains;

namespace ScaleVoting.BlockChainClient.Client
{
    public class BcClient
    {
        private WebClient Client { get; }
        private IBlockChainCorrector Corrector { get; }

        public BcClient()
        {
            Client = new WebClient
            {
                NodesUri = NodeReplics.NodeReplicsAddresses,
                TimeoutInMsec = 2000
            };

            Corrector = new BlockChainCorrector();
        }

        private async Task<IEnumerable<Block>> GetChain()
        {
            var json = await Client.GetResponseFromRequestTo(NodeApi.Chain);
            var nodeResponse = JsonConvert.DeserializeObject<NodeResponse>(json);
            return JsonConvert.DeserializeObject<List<Block>>(nodeResponse.DataRows["Chain"]);
        }

        public async Task<string> SendVoteToBlockChain(Vote vote)
        {
            var transaction = new Transaction(vote);
            var json = JsonConvert.SerializeObject(transaction);

            return await Client.GetResponseFromRequestWithJsonTo(NodeApi.NewVote, json);
        }

        public async Task<IEnumerable<Vote>> GetVotesFromDate(string startTimeStamp)
        {
            var chain = await GetChain();
            chain = Corrector.Fix(chain.ToArray(), startTimeStamp);
            var transactions = BlockChainExtension.ExtractTransactions(chain);

            return transactions.Select(transaction => transaction.ToVote());
        }
    }
}