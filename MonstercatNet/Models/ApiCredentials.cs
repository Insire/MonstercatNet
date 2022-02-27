using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class ApiCredentials
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
    }
}
