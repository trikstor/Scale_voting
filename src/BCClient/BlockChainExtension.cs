using System;
using BlockChainMachine.Core;

namespace BCClient
{
    public class BlockChainExtension
    {
        public Block[] blockChain { get; }

        public int GetBlockIndexWithTimestamp(TimeSpan timestamp)
        {
            const int step = 3;

            for (var counter = 0; counter < blockChain.Length; counter += step)
            {
                if (TimeSpan.Parse(blockChain[counter].TimeStamp) < timestamp)
                {
                    continue;
                }

                var limit = counter >= step ? counter - step : 0;
                while (counter > limit)
                {
                    if (TimeSpan.Parse(blockChain[counter].TimeStamp) <= timestamp)
                    {
                        return counter;
                    }
                    counter--;
                }
            }
            return -1;
        }
    }
}