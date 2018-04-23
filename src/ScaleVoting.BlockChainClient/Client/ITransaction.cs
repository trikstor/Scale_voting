namespace ScaleVoting.BlockChainClient.Client
{
    public interface ITransaction
    {
        string Data { get; set; }
        bool HasValidData { get; set; }
        string UserHash { get; set; }
        byte[] Signature { get; set; }
    }
}
