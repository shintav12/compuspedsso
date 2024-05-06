using CompuSPED.Common.Base;
using CompuSPED.Common.Lumen;
using CompuSPED.Service.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompuSPED.Service
{
    public class AuthService : BaseService
    {
        public AuthService() 
        {
        }

        public async Task<ServiceResponse<AccessToken>> Authenticate()
        {

            async Task<AccessToken> FuncAsync()
            {
                using (var client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes("kJ2BBwKXnRnQDWi9r4MKmyEcDPaBIyd5:ZX610e6mAubZ2adiM4Xu6T6HS7423J8d");
                    var dict = new Dictionary<string, string>();
                    dict.Add("grant_type", "client_credentials");
                    dict.Add("scope", "https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster-demographics.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/resource.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/roster.readonly https://purl.imsglobal.org/spec/or/v1p1/scope/gradebook.readonly");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11
                                                           | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    var req = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/oauth/token") { Content = new FormUrlEncodedContent(dict) };

                    var response = await client.SendAsync(req);
                    var result = await response.Content.ReadAsAsync<AccessToken>();
                    return result;
                }
            };
            return await ExecuteAsync(FuncAsync);
        }
    }
}
