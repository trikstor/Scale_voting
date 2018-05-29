using System.Configuration;

namespace ScaleVoting.BlockChainClient.Client
{
    public class NodeApi
    {
        public static readonly string Chain = ConfigurationManager.AppSettings["GetChainRequest"];

        public static readonly string NewVote = ConfigurationManager.AppSettings["NewVoteRequest"];
    }
}