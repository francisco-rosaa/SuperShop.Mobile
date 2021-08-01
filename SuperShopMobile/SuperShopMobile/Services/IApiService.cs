using System.Threading.Tasks;
using SuperShopMobile.Models;

namespace SuperShopMobile.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string apiBaseUrl, string apiServicePrefix, string apiController);
    }
}
