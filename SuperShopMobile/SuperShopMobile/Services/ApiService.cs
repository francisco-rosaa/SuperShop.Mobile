using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SuperShopMobile.Models;

namespace SuperShopMobile.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetListAsync<T>(string apiBaseUrl, string apiServicePrefix, string apiController)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(apiBaseUrl),
                };

                var url = $"{apiServicePrefix}{apiController}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
