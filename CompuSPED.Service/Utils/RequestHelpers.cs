using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompuSPED.Service.Utils
{
    public static class HttpClientHelper
    {
        public static async Task<T> PostAsync<T>(this string baseUri, string uri, object data, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response =
                client.PostAsync($"{baseUri}/{uri}" , GetStringContent(data)).Result)
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                return default(T);
            }
        }

        public static async Task<T> DeleteAsync<T>(this string baseUri, string uri, object data, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await client.DeleteAsync($"{baseUri}/{uri}"))
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsAsync<T>();
                        return result;
                    }
                return default(T);
            }
        }

        public static async Task<T> PutAsync<T>(this string baseUri, string uri, object data, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response =
                    client.PutAsync($"{baseUri}/{uri}", GetStringContent(data)).Result)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsAsync<T>();
                        return result;
                    }
                    return default(T);
                }
            }
        }

        public static async Task<T> GetAsync<T>(this string baseUri, string uri, string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response =
                    client.GetAsync($"{baseUri}/{uri}").Result)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsAsync<T>();
                        return result;
                    }
                    return default(T);
                    
                }
            }
        }
        public static StringContent GetStringContent(object value)
        {
            var json = JsonConvert.SerializeObject(value);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
