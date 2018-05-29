using System.Collections.Generic;
using System.Net;

namespace ScaleVoting.BlockChainClient.Client
{
    struct NodeResponse
    {
        public string Host { get; set; }
        public string ResponseString { get; set; }
        public HttpStatusCode HttpCode { get; set; }
        public Dictionary<string, string> DataRows { get; set; }
    }
}