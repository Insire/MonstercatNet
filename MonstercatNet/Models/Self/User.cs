using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class User
    {
        public bool Archived { get; set; }
        public Attributes? Attributes { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmailVerificationCode { get; set; } = string.Empty;
        public string EmailVerificationStatus { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public bool GivenDownloadAccess { get; set; }
        public string GoogleMapsPlaceId { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string LastSeen { get; set; } = string.Empty;
        public bool LastUpdateBenefitsGold { get; set; }
        public float LocationLat { get; set; }
        public float LocationLng { get; set; }
        public int MaxLicenses { get; set; }
        public Guid MyLibrary { get; set; }
        public string PasswordVerificationCode { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public string PlaceNameFull { get; set; } = string.Empty;
        public string ProvSt { get; set; } = string.Empty;
        public string ProvinceState { get; set; } = string.Empty;
        public string Pronouns { get; set; } = string.Empty;
        public string SaySongTwitchName { get; set; } = string.Empty;
        public string TwoFactorId { get; set; } = string.Empty;
        public string TwoFactorPendingId { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string Username { get; set; } = string.Empty;
        public object[]? Features { get; set; }
        public UserSettings? Settings { get; set; }
        public bool HasGold { get; set; }
        public object? FreeGoldAt { get; set; }
        public bool FreeGold { get; set; }
        public string FreeGoldReason { get; set; } = string.Empty;
        public bool HasDownload { get; set; }
        public bool HasPassword { get; set; }
    }
}
