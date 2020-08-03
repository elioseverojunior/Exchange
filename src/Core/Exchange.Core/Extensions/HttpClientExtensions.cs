using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Exchange.Core.Interfaces;

namespace Exchange.Core.Extensions
{
    /// <summary>
    /// Http Client Extension
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> RequestAsync<T>(this T client, 
            string queryString = "") where T : HttpClient
        {
            client.SetHttpClientDefaultRequestHeaders();
            var httpResponseMessage = await client.GetAsync(queryString);
            httpResponseMessage.EnsureSuccessStatusCode();
            return httpResponseMessage;
        }

        /// <summary>
        /// Http Request Async
        /// </summary>
        /// <param name="client"></param>
        /// <param name="httpMethod"></param>
        /// <param name="payLoad"></param>
        /// <param name="apiConfigSettings"></param>
        /// <param name="queryString"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> RequestAsync<T>(
            this T client,
            HttpMethod httpMethod,
            IApiConfigurationSettings apiConfigSettings, 
            string queryString = null, 
            List<KeyValuePair<string, string>> headers = null, 
            string contentType = null, 
            HttpContent payLoad = null) where T : HttpClient
        {
            using (var cancellationToken =
                new CancellationTokenSource(TimeSpan.FromMilliseconds(apiConfigSettings.TimeoutInMs)))
            {
                var startedTime = DateTime.Now;
                try
                {
                    client.SetHttpClientDefaultRequestHeaders();
                    var requestMessage = SetHttpClientRequestMessageHeaders(headers, contentType, 
                        HttpClientRequestMessage(httpMethod, queryString, payLoad));
                    var httpResponseMessage = await client.SendAsync(requestMessage, cancellationToken.Token);
                    httpResponseMessage.EnsureSuccessStatusCode();
                    return httpResponseMessage;
                }
                catch(OperationCanceledException e)
                {
                    if(!cancellationToken.Token.IsCancellationRequested)
                        throw new TimeoutException($"An HTTP request to {apiConfigSettings.BaseUrl} timed out ({(int)new TimeSpan(apiConfigSettings.TimeoutInMs).TotalSeconds} seconds.\n\n{e.Message})");
                    throw;
                }
            }
        }

        private static HttpRequestMessage SetHttpClientRequestMessageHeaders(List<KeyValuePair<string, string>> headers, string contentType, HttpRequestMessage requestMessage)
        {
            if (headers != null && string.IsNullOrEmpty(contentType))
            {
                headers = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Content-type", contentType)
                };
                headers.ForEach(kvp => { requestMessage.Headers.Add(kvp.Key, kvp.Value); });
            }

            return requestMessage;
        }

        private static HttpRequestMessage HttpClientRequestMessage(HttpMethod httpMethod, string queryString = null, HttpContent payLoad = null)
        {
            var requestMessage = new HttpRequestMessage();
            if (httpMethod == null) return requestMessage;
            if (queryString == null) return requestMessage;
            requestMessage = new HttpRequestMessage(httpMethod, queryString);
            if (payLoad != null)
            {
                requestMessage.Content = payLoad;
            }
            return requestMessage;
        }

        private static void SetHttpClientDefaultRequestHeaders<T>(this T Client) where T : HttpClient
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }
    }
}