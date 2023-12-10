using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Timelogger.Api.Tests.Lib
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> Create<T>(this HttpClient httpClient, string path, T obj)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var postResponse = await httpClient.PostAsync($"api/{path}", jsonContent);
            return postResponse;
        }

        public static async Task<HttpResponseMessage> Update<T>(this HttpClient httpClient, string path, T obj)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var postResponse = await httpClient.PatchAsync($"api/{path}", jsonContent);
            return postResponse;
        }

        public static async Task<T> Get<T>(this HttpClient httpClient, string path)
        {
            var getResponse = await httpClient.GetAsync($"api/{path}");
            var getResponseBody = await getResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(getResponseBody);
            return result;
        }
    }
}


