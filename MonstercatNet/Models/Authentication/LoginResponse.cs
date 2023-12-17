using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class LoginResponse
    {
        [JsonPropertyName("Needs2FA")]
        public bool Needs2FA { get; set; }

        /// <summary>
        /// Specifies, which Time-based One-time Password (TOTP) to use.
        /// </summary>
        /// <remarks>
        ///there are also E-Mail and SMS, but they are both depricated.
        /// </remarks>
        [JsonPropertyName("DefaultAuthType")]
        public string? AuthTypeToUse { get; set; } = "TOTP";

        /// <summary>
        /// This is being filled, when login fails.
        /// </summary>
        [JsonPropertyName("AuthData")]
        public AuthenticationRequirements? AuthData { get; set; } = new AuthenticationRequirements();

        [JsonIgnore]
        public bool IsLoggedIn => AuthData is null;
    }
}
