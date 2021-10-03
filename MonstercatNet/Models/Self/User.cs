using System;

namespace SoftThorn.MonstercatNet
{
    public sealed class User
    {
        public bool Archived { get; set; }
        public Attributes? Attributes { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; } = "";
        public string Continent { get; set; } = "";
        public string Country { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; } = "";
        public string EmailVerificationCode { get; set; } = "";
        public string EmailVerificationStatus { get; set; } = "";
        public string FirstName { get; set; } = "";
        public bool GivenDownloadAccess { get; set; }
        public string GoogleMapsPlaceId { get; set; } = "";
        public Guid Id { get; set; }
        public string LastName { get; set; } = "";
        public string LastSeen { get; set; } = "";
        public bool LastUpdateBenefitsGold { get; set; }
        public float LocationLat { get; set; }
        public float LocationLng { get; set; }
        public int MaxLicenses { get; set; }
        public string MyLibrary { get; set; } = "";
        public string PasswordVerificationCode { get; set; } = "";
        public string PlaceName { get; set; } = "";
        public string PlaceNameFull { get; set; } = "";
        public string ProvSt { get; set; } = "";
        public string ProvinceState { get; set; } = "";
        public string Pronouns { get; set; } = "";
        public string SaySongTwitchName { get; set; } = "";
        public string TwoFactorId { get; set; } = "";
        public string TwoFactorPendingId { get; set; } = "";
        public DateTime UpdatedAt { get; set; }
        public string Username { get; set; } = "";
        public object[]? Features { get; set; }
        public UiSettings Settings { get; set; } = new UiSettings();
        public bool HasGold { get; set; }
        public object? FreeGoldAt { get; set; }
        public bool FreeGold { get; set; }
        public string FreeGoldReason { get; set; } = "";
        public bool HasDownload { get; set; }
        public bool HasPassword { get; set; }
    }
}
