using System.Collections.Generic;
using ScaleVoting.Domains;

namespace ScaleVoting.BlockChainClient.Client
{
    public interface IBlockChainCorrector
    {
        Question Question { get; }
        string[] AllowedOptions { get; }

        IEnumerable<Block> Fix(IEnumerable<Block> blockChain);
    }
}
