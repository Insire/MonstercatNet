using Refit;
using SoftThorn.MonstercatNet.Util;
using System.Text.Json;

namespace SoftThorn.MonstercatNet
{
    public abstract class MonstercatBase
    {
        protected static RefitSettings Settings { get; } = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(CreateOptions())
        };

        protected static JsonSerializerOptions CreateOptions()
        {
            var options = new JsonSerializerOptions();
            options.AddContext<MonstercatContext>();

            return options;
        }
    }
}
