using EscrowMAUI.Models;
using OneOf;
using System.Net.Http.Json;
using System.Text.Json;

namespace EscrowMAUI.Services;

public class AuthServices(HttpClient httpClient)
{
    public async Task<OneOf<User, Problem>> LoginAsync(string email, string password, string userType = "Consumer")
    {
        var payload = new
        {
            email,
            password
        };

        var jsonString = JsonSerializer.Serialize(payload);
        var postString = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        var result = await httpClient.PostAsync($"{userType}/login", postString);

        if (result.IsSuccessStatusCode)
        {
            User authenticatedUser = await result.Content.ReadFromJsonAsync<User>();
            return authenticatedUser;
        }
        else
        {
            Problem problem = await result.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }
}
