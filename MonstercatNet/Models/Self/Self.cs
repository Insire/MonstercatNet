namespace SoftThorn.MonstercatNet
{
    public sealed class Self
    {
        public object[]? Features { get; set; }
        public UserSettings Settings { get; set; } = new UserSettings();
        public User User { get; set; } = new User();
    }
}
