using Newtonsoft.Json;
using SentencesFrontApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SentencesFrontApp.Core
{
    public class CoreApp
    {
        private readonly HttpClient _httpClient;
        
        public CoreApp()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(Uri requestUri)
        {

            var response = await _httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);

        }

        public async Task<T> PostAsync<T>(Uri requestUri, T content)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(requestUri.ToString(), CreateHttpContent<T>(content));
            var data = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(data);
        }



        private void AddHeaders()   //add sample headers
        {
            _httpClient.DefaultRequestHeaders.Add("Name", "Pawel");
        }

        private HttpContent CreateHttpContent<T>(T content)     //createcontent to Post method
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
      
        public Uri CreateRequestUri(string apiMethod)           //create Uri (address + apiMethod)
        {
            var baseUri = new Uri(ApplicationSettings.baseUrl);
            return new Uri(baseUri + apiMethod);
        }
    }
}
