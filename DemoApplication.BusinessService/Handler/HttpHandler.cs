using DemoApplication.BusinessServices.Interfaces.Handler;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApplication.BusinessService.Handler
{
    public class HttpHandler : IHttpHandler
    {
        private HttpClient client = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await client.GetAsync(url);
        }
    }
}
