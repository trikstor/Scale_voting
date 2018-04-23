using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScaleVoting.BlockChainClient.Client
{
    public class WebClient
    {
        public string[] AllNodeUri;
        public long TimeoutInMsec { get; set; }
        private TimeSpan Timeout => TimeSpan.FromMilliseconds(TimeoutInMsec / AllNodeUri.Length);
        //private ILogger Logger => LogManager.GetCurrentClassLogger();

        public async Task<string> GetResponseFromRequestTo(string requestPartOfUri) =>
            await GetResponseFromRequest(requestPartOfUri);

        public async Task<string> GetResponseFromRequestWithJsonTo(string requestPartOfUri, string data) =>
            await GetResponseFromRequest(requestPartOfUri, data, "application/json; charset=utf-8", "POST");

        public async Task<string> GetResponseFromRequest(
            string requestPartOfUri, string data = null, string dataType = null, string method = "GET")
        {
            try
            {
                foreach (var nodeUri in AllNodeUri)
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
            catch (Exception e)
            {
                //Logger.Error(e, "ошибка на клиенте получения данных с ноды");
            }
            throw new TimeoutException("Ни одна нода из заданных не ответила");
        }

        private async Task<string> GetResponse(WebRequest request)
        {
            using (var response = await request.GetResponseAsync())
            {
                var result =
                    await new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEndAsync();
                return result;
            }
        }
    }
}