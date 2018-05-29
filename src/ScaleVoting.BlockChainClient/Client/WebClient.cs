using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Client
{
    public class WebClient
    {
        public string[] NodesUri;
        public long TimeoutInMsec { private get; set; }

        private TimeSpan Timeout =>
            TimeSpan.FromMilliseconds((double) TimeoutInMsec / NodesUri.Length);

        public async Task<string> GetResponseFromRequestTo(string requestPartOfUri)
        {
            return await GetResponseFromRequest(requestPartOfUri);
        }

        public async Task<string> GetResponseFromRequestWithJsonTo(
            string requestPartOfUri, string data)
        {
            return await GetResponseFromRequest(requestPartOfUri, data,
                                                "application/json; charset=utf-8", "POST");
        }

        private async Task<string> GetResponseFromRequest(string requestPartOfUri,
                                                          string data = null, string dataType = null,
                                                          string method = "GET")
        {
            try
            {
                foreach (var nodeUri in NodesUri)
                {
                    var request = WebRequest.Create(nodeUri + requestPartOfUri);
                    if (dataType != null)
                    {
                        request.ContentType = dataType;
                    }

                    request.Method = method;

                    if (data != null)
                    {
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }
                    }

                    var task = GetResponse(request);
                    await Task.WhenAny(task, Task.Delay(Timeout));
                    if (task.IsCompleted)
                    {
                        return task.Result;
                    }
                }
            }
            catch (Exception)
            {
                //TODO Logger error
            }

            throw new TimeoutException("Ни одна нода из заданных не ответила");
        }

        private static async Task<string> GetResponse(WebRequest request)
        {
            using (var response = await request.GetResponseAsync())
            {
                var result =
                    await new StreamReader(
                        response.GetResponseStream() ?? throw new InvalidOperationException(),
                        Encoding.UTF8).ReadToEndAsync();
                return result;
            }
        }
    }
}