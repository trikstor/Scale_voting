using System.Collections.Generic;
using CsvHelper.Configuration;

namespace BlockChainMachine.Core
{
    /// <summary>
    ///     Один блок целой цепи
    /// </summary>
    public struct Block
    {
        public int Index;
        public List<Answer> Transactions;
        public string PreviousHash;
        public string TimeStamp;

        public override string ToString()
        {
            return $"Block: No. {Index}, with {Transactions.Count} transactions.\n" +
                   $"Previous hash: {PreviousHash}, created on {TimeStamp}.";
        }
    }
    
    public sealed class BlockMap : ClassMap<Block>
    {
        public BlockMap()
        {
            Map(m => m.Index);
            Map(m => m.TimeStamp);
            Map(m => m.PreviousHash);
        }
    }
    
    public sealed class TransactionMap : ClassMap<Answer>
    {
        public TransactionMap()
        {
            Map(m => m.QuestionId);
            Map(m => m.UserHash);
            Map(m => m.OptionId);
        }
    }
}