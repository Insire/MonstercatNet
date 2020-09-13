using Newtonsoft.Json;

namespace SoftThorn.MonstercatNet
{
    public sealed class ApiCredentials
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = string.Empty;
    }
}
