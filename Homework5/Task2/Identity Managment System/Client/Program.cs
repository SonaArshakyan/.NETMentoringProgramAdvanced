using Newtonsoft.Json;
using Client;
class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var tokenEndpoint = "https://localhost:5001/connect/token";
        var accessToken = await GetAccessToken(client, tokenEndpoint);
        Console.WriteLine($"Access Token: {accessToken}");
    }

    static async Task<string> GetAccessToken(HttpClient client, string tokenEndpoint)
    {
        var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
        {

                 { "grant_type", "client_credentials" },
                 { "client_id", "client" },
                 { "client_secret", "client_secret" },
                 { "scope", "read create update delete" }
        });

        var response = await client.PostAsync(tokenEndpoint, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<Token>(responseContent);
        return tokenResponse.AccessToken;
    }
}