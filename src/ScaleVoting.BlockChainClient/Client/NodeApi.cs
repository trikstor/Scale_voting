using System.Configuration;

namespace ScaleVoting.BlockChainClient.Client
{
    public class NodeApi
    {
        public static readonly string Chain = 
            ConfigurationSettings.AppSettings["GetChainRequest"];
        public static readonly string NewTransaction = 
            ConfigurationSettings.AppSettings["NewTransactionRequest"];
    }
}
