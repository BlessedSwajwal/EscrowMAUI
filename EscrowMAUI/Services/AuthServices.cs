using EscrowMAUI.Models;
using EscrowMAUI.Models.DTOs;
using OneOf;
using System.Net.Http.Headers;
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

    public async Task<OneOf<User, Problem>> Register(User user, string userType = "Consumer")
    {
        var payload = new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            user.Password
        };

        var jsonString = JsonSerializer.Serialize(payload);
        var postString = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        var result = await httpClient.PostAsync($"{userType}/register", postString);

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

    public async Task<OneOf<UserDetailResponse, Problem>> GetUserDetail()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Default.Get<string>(Constants.Constants.TokenKeyConstant, string.Empty));

        var response = Preferences.Default.Get<string>(Constants.Constants.UserType, "").ToLower().Equals("consumer") ? await httpClient.GetAsync("Consumer/details") : await httpClient.GetAsync("Provider/details");

        if (response.IsSuccessStatusCode)
        {
            var userResponse = await response.Content.ReadFromJsonAsync<UserDetailResponse
                >();
            return userResponse;
        }
        else
        {
            Problem problem = await response.Content.ReadFromJsonAsync<Problem>();
            return problem;
        }
    }
}
