using EscrowMAUI.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace EscrowMAUI.Services;

public static class TokenDecode
{
    public static User ReadJwtTokenContent(string token)
    {
        var content = token.Split('.')[1];


        var jsonPayload = Encoding.UTF8.GetString(
            Decode(content));

        var userObj = JsonDocument.Parse(jsonPayload).RootElement;
        var user = new User
        {

            Id = Guid.Parse(userObj.GetProperty(ClaimTypes.NameIdentifier).GetString()),
            FirstName = userObj.GetProperty(ClaimTypes.GivenName).GetString(),
            LastName = userObj.GetProperty(ClaimTypes.Name).GetString(),
            Email = userObj.GetProperty(ClaimTypes.Email).GetString(),
            Phone = userObj.GetProperty(ClaimTypes.MobilePhone).GetString()
        };
        return user;

    }
    static byte[] Decode(string input)
    {
        var output = input;
        output = output.Replace('-', '+'); // 62nd char of encoding
        output = output.Replace('_', '/'); // 63rd char of encoding
        switch (output.Length % 4) // Pad with trailing '='s
        {
            case 0: break; // No pad chars in this case
            case 2: output += "=="; break; // Two pad chars
            case 3: output += "="; break; // One pad char
            default: throw new System.ArgumentOutOfRangeException("input", "Illegal base64url string!");
        }
        var converted = Convert.FromBase64String(output); // Standard base64 decoder
        return converted;
    }
}
