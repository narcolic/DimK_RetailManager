using DKRDesktopUI.Library.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public SaleEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSaleAsync(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Log Successful call
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}