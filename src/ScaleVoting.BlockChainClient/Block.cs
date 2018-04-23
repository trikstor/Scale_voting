using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient
{
    public class Block
    {
        public int Index;
        public List<ITransaction> Transactions;
        public string PreviousHash;
        public string TimeStamp;
    }
}
