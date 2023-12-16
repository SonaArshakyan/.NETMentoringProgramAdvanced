using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using Newtonsoft.Json;

var client = new HttpClient();
var tokenEndpoint = "https://localhost:5001/connect/token";

var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
{

    { "grant_type", "client_credentials" },
    { "client_id", "manager_client" },
    { "client_secret", "manager_secret" },
    { "scope", "read create update delete" }
});

var response = await client.PostAsync(tokenEndpoint, requestContent);

var responseContent = await response.Content.ReadAsStringAsync();
var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
var identityToken = tokenResponse.IdentityToken;
