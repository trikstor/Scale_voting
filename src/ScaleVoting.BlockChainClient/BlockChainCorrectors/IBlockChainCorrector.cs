using System.Collections.Generic;
using ScaleVoting.BlockChainClient.BlockChainCore;

namespace ScaleVoting.BlockChainClient.BlockChainCorrectors
{
    public interface IBlockChainCorrector
    {
        IEnumerable<Block> Fix(Block[] chain, string startTimeStamp);
    }
}