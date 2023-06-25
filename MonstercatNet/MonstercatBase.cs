using Refit;

namespace SoftThorn.MonstercatNet
{
    public abstract class MonstercatBase
    {
        protected static RefitSettings Settings { get; } = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer()
        };
    }
}
