using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScaleVoting.Domains;

namespace ScaleVoting.BlockChainClient.Transaction
{
    class Transaction : ITransaction
    {
        public string Data { get; set; }
        public bool HasValidData { get; set; }
        public string UserHash { get; set; }
        public byte[] Signature { get; set; }

        public Answer ToAnswer()
        {
            var tt = JsonConvert.DeserializeObject<Answer>(Data);
            return tt;
        }
    }
}
