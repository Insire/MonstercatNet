using Cake.Frosting;

namespace Build
{
    [TaskName("Push")]
    [IsDependentOn(typeof(PushRemoteTask))]
    [IsDependentOn(typeof(PushLocallyTask))]
    public sealed class PushTask : FrostingTask<BuildContext>
    {
    }
}
