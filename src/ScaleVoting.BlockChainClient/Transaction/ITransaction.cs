using ScaleVoting.Domains;

namespace ScaleVoting.BlockChainClient.Transaction
{
    public interface ITransaction
    {
        string Data { get; set; }
        bool HasValidData { get; set; }
        string UserHash { get; set; }
        byte[] Signature { get; set; }

        Answer ToAnswer();
    }
}
