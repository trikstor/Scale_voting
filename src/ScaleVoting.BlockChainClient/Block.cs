using System.Collections.Generic;
using ScaleVoting.BlockChainClient.Transaction;

namespace ScaleVoting.BlockChainClient
{
    public class Block
    {
        public int Index { get; set; }
        public List<ITransaction> Transactions { get; set; }
        public string PreviousHash { get; set; }
        public string TimeStamp { get; set; }

        public override string ToString()
        {
            return $"Block: No. {Index}, with {Transactions.Count} transactions.\n" +
                   $"Previous hash: {PreviousHash}, created on {TimeStamp}.";
        }
    }
}
