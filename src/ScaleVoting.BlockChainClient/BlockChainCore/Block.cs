using System.Collections.Generic;

namespace ScaleVoting.BlockChainClient.BlockChainCore
{
    public struct Block
    {
        public int Index;
        public string PreviousHash;
        public string TimeStamp;
        public List<Transaction> Transactions;
    }
}