using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class ApiCredentials
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("Auth.TOTP")]
        public string? TOTP { get; set; }
    }
}
