using System.Configuration;

namespace ScaleVoting.BlockChainClient.Client
{
    public class NodeApi
    {
        public static readonly string Chain =
            ConfigurationManager.AppSettings["GetChainRequest"];
        public static readonly string NewTransaction =
            ConfigurationManager.AppSettings["NewTransactionRequest"];
    }
}
