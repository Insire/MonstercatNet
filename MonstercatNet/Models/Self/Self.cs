using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class Self
    {
        public bool Admin { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DiscordId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public object[]? EmailOptins { get; set; }
        public string EmailVerificationStatus { get; set; } = string.Empty;
        public bool FreeGold { get; set; }
        public string GoogleMapsPlaceId { get; set; } = string.Empty;
        public bool HasDownload { get; set; }
        public bool HasGold { get; set; }
        public string Id { get; set; } = string.Empty;
        public string LastSeen { get; set; } = string.Empty;
        public int MaxLicenses { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public string PlaceNameFull { get; set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        public Settings? Settings { get; set; }
        public Subscription? Subscription { get; set; }
        public string TwoFactorState { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
