using DemoApplication.ApiModels.Configuration;
using DemoApplication.BusinessServices.Interfaces.Handler;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApplication.BusinessService.Handler
{
    public class WebApiCallerService : ApiCallerService, IWebApiCallerService
    {
        public WebApiCallerService(HttpClient httpClient)
            :base(httpClient)
        {
        }

        public async Task<R> GetDataAsync<R>(string url)
        {
            return await GetAsync<R>(url);
        }
    }
}
