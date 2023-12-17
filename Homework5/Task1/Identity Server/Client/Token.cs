using Newtonsoft.Json;


namespace Client;

public class Token
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
