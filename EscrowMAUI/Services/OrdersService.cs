using EscrowMAUI.Models;
using EscrowMAUI.Models.DTOs;
using OneOf;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EscrowMAUI.Services;

public class OrdersService(HttpClient httpClient)
{
    public async Task<OneOf<List<Order>, Problem>> GetAllUserOrders()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

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

    public async Task<OneOf<SingleOrderDetail, Problem>> SubmitOrder(CreateOrderDTO createOrderDTO)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var jsonPayload = JsonSerializer.Serialize(createOrderDTO);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("Order/Create", stringContent);
        if (response.IsSuccessStatusCode)
        {
            var order = await response.Content.ReadFromJsonAsync<SingleOrderDetail>();
            return order;
        }
        else
        {
            Problem problem = await response.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }

    public async Task<OneOf<SingleOrderDetail, Problem>> GetOrderDetail(Guid orderId)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = await httpClient.GetAsync($"Order/{orderId}");

        if (response.IsSuccessStatusCode)
        {
            var order = await response.Content.ReadFromJsonAsync<SingleOrderDetail>();
            return order;
        }
        else
        {
            Problem problem = await response.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }

    public async Task<OneOf<List<Order>, Problem>> GetAllCreatedOrders()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = await httpClient.GetAsync("Order");

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

    public async Task<OneOf<List<Order>, Problem>> GetBidsSelectedOrders()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = await httpClient.GetAsync("Provider/GetSelectedBids");

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

    public async Task<bool> AcceptBid(Guid OrderId, Guid BidId)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = await httpClient.GetAsync($"Order/{OrderId}/AcceptBid?BidId={BidId}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<OneOf<Bid, Problem>> CreateBid(CreateBidDTO BidDTO)
    {
        Object payload = new
        {
            OrderId = BidDTO.OrderId,
            ProposedAmount = BidDTO.ProposedAmountInPaisa,
            BidDTO.Comment
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = await httpClient.PostAsync("Provider/CreateBid", stringContent);
        if (response.IsSuccessStatusCode)
        {
            var bid = await response.Content.ReadFromJsonAsync<Bid>();
            return bid;
        }
        else
        {
            var problem = await response.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }
}
