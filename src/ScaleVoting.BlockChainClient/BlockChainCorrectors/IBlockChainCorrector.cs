using System.Collections.Generic;
using ScaleVoting.Domains;

namespace ScaleVoting.BlockChainClient.BlockChainCorrectors
{
    public interface IBlockChainCorrector
    {
        IEnumerable<Block> Fix(Block[] blockChain, string startTimestamp);
    }
}
