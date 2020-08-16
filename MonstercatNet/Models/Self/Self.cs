using System;

namespace SoftThorn.MonstercatNet
{
    public class Self
    {
        public bool admin { get; set; }
        public DateTime birthday { get; set; }
        public DateTime createdAt { get; set; }
        public string discordId { get; set; }
        public string email { get; set; }
        public object[] emailOptins { get; set; }
        public string emailVerificationStatus { get; set; }
        public bool freeGold { get; set; }
        public string googleMapsPlaceId { get; set; }
        public bool hasDownload { get; set; }
        public bool hasGold { get; set; }
        public string id { get; set; }
        public string lastSeen { get; set; }
        public int maxLicenses { get; set; }
        public string placeName { get; set; }
        public string placeNameFull { get; set; }
        public string realName { get; set; }
        public Settings settings { get; set; }
        public Subscription subscription { get; set; }
        public string twoFactorState { get; set; }
        public string username { get; set; }
    }
}
