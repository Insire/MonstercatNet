using System.Text.Json.Serialization;

namespace SoftThorn.MonstercatNet
{
    public sealed class LoginUser
    {
        /// <summary>
        /// remains empty.
        /// </summary>
        [JsonPropertyName("Id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// will contain the submitted e-mail.
        /// </summary>
        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;
    }
}
