using EscrowMAUI.Constants;
using EscrowMAUI.Models;
using OneOf;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EscrowMAUI.Services;

public class OrdersService(HttpClient httpClient)
{
    public async Task<OneOf<List<Order>, Problem>> GetAllUserOrders()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(EscrowConstants.TokenKeyConstant, string.Empty));

        var response = await httpClient.GetAsync("Consumer/GetAllOrder");

        if (response.IsSuccessStatusCode)
        {
            var order = await response.Content.ReadFromJsonAsync<List<Order>>();
            return order;
        }
        else
        {
            Problem problem = await response.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }
}
