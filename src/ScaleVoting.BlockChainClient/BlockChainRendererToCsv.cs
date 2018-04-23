using System.IO;
using BlockChainMachine.Core;
using CsvHelper;

namespace ScaleVoting.BlockChainClient
{
    public class BlockChainRendererToCsv
    {
        public void Render(Block[] blockChain, StreamWriter streamWriter)
        {
            var csvWriter = new CsvWriter(streamWriter);
            csvWriter.Configuration.RegisterClassMap<BlockMap>();
            csvWriter.Configuration.RegisterClassMap<TransactionMap>();
            foreach (var block in blockChain)
            {
                csvWriter.WriteField("Индекс блока");
                csvWriter.WriteField("Временной идентификатор");
                csvWriter.WriteField("Хеш от предыдущего блока");
                csvWriter.NextRecord();
                csvWriter.WriteRecord(block);
                csvWriter.NextRecord();
                csvWriter.WriteField("Идентификатор опроса");
                csvWriter.WriteField("Идентификатор пользователя");
                csvWriter.WriteField("Идентификатор варианта ответа");
                csvWriter.NextRecord();
                csvWriter.WriteRecords(block.Transactions);
                csvWriter.WriteField("----------");
                csvWriter.WriteField("----------");
                csvWriter.WriteField("----------");
                csvWriter.NextRecord();
            }
        }
    }
}