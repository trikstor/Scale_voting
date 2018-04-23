namespace ScaleVoting.BlockChainClient.Client
{
    class Transaction : ITransaction
    {
        public string Data { get; set; }
        public bool HasValidData { get; set; }
        public string UserHash { get; set; }
        public byte[] Signature { get; set; }
    }
}
