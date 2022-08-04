using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.BusinessServices.Interfaces.Handler
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string url);

        Task<HttpResponseMessage> GetAsync(string url);
    }
}
