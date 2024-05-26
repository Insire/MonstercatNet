using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class AuthenticationRequirements
    {
        [JsonPropertyName("Email")]
        public LoginUser User { get; set; } = new LoginUser();

        [JsonPropertyName("TOTP")]
        public TOTP TOTP { get; set; } = new TOTP();
    }
}
