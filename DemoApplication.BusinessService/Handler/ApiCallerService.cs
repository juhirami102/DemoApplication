using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApplication.BusinessService.Handler
{
    public abstract class ApiCallerService 
    {
        protected readonly HttpClient _httpCient;
        private readonly JsonSerializerSettings _jsonSerializerSettings = null;

        public ApiCallerService(HttpClient httpClient)
        {
            _httpCient = httpClient;
        }
        public ApiCallerService(HttpClient httpClient,
            JsonSerializerSettings jsonSerializerSettings)
        {
            _httpCient = httpClient;
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task<T> GetAsync<T>(string uri)
       {
            var response = await _httpCient.GetAsync(uri);

            return await GetDataAsync<T>(response);
       }

        protected async Task<T> GetDataAsync<T>(HttpResponseMessage response)
        {
            await ValidateResponseAsync(response);
            var data = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return DesializeString<T>(data);

            return JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings);
        }

        private T DesializeString<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings);
            }
            catch
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
        }

        protected virtual async Task ValidateResponseAsync(HttpResponseMessage response)
        {
            if(!response.IsSuccessStatusCode)
            {
           
                if(response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var exceptionClass = response.Headers.GetValues("ExceptionClass").First();
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var exceptionType = Type.GetType(exceptionClass);
                        var exception = JsonConvert.DeserializeObject(content, exceptionType);
                        //TODO : Custom Exception Class need to Implemented
                    }
                }
            }
        }

        private void ThrowApiException<T>(T exception) where T : Exception => throw exception;
    }
}
