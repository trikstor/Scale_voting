using System.IO;
using System.Threading.Tasks;
using BlockChainMachine.Core;
using Newtonsoft.Json;

namespace BCClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            var json = JsonConvert.SerializeObject(
                new Answer("1", "aaaaa", "bbbbb");
            var nodeClient = new UstalovGeneralNodeClient()
            {
                AllNodeUri = new[] {"http://127.0.0.1:3344"},
                TimeoutInMsec = 2000
            };
            
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));
            Task.WaitAll(nodeClient.GetResponseFromRequestWithJsonTo("/bc/newtrans", json));

            var task = nodeClient.GetResponseFromRequestTo("/bc/chain");
            Task.WaitAll(task);

            var jsonChain = task.Result;
            var chain = JsonConvert.DeserializeObject<Block[]>(jsonChain);
            var stat = new BlockChainStatisticsProvider("aaaaaa", new[] {"1"}).GetStatisticsFor(chain);
            var sw = new StreamWriter("testTable.csv");
            new BlockChainRendererToCsv().Render(chain, sw);
            sw.Close();
            */
        }
    }
}