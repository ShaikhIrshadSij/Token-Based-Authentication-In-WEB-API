using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TokenBasedAuthentication.Helper
{
    public class APIServices
    {
        public static readonly string _baseUrl = ConfigurationManager.AppSettings["WEBAPIURL"];
        public static async Task<T> Post<T>(string url, MultipartFormDataContent contentValue)
        {
            using (HttpClient client = new HttpClient())
            {
                if (ClaimsPrincipal.Current.Identities.Where(x => !string.IsNullOrEmpty(x.Name)).ToList().Count > 0)
                {
                    var authToken = ClaimsPrincipal.Current.Identities.First().Claims.FirstOrDefault(x => x.Type == "AccessToken").Value;
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", authToken);
                    }
                }
                client.BaseAddress = new Uri(_baseUrl);
                client.Timeout = TimeSpan.FromMinutes(10);
                HttpResponseMessage result = await client.PostAsync(url, contentValue);
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        public static async Task<T> Get<T>(string url, string token = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (ClaimsPrincipal.Current.Identities.Where(x => !string.IsNullOrEmpty(x.Name)).ToList().Count > 0)
                {
                    var authToken = ClaimsPrincipal.Current.Identities.First().Claims.FirstOrDefault(x => x.Type == "AccessToken").Value;
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", authToken);
                    }
                }
                client.BaseAddress = new Uri(_baseUrl);
                client.Timeout = TimeSpan.FromMinutes(10);
                HttpResponseMessage result = await client.GetAsync(url);
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }
    }
}