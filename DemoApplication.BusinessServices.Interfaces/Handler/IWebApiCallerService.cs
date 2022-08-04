using System.Threading.Tasks;

namespace DemoApplication.BusinessServices.Interfaces.Handler
{
    public interface IWebApiCallerService 
    {
        Task<R> GetDataAsync<R>(string url);
    }
}
