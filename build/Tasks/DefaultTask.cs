using Cake.Frosting;

namespace Build
{
    [TaskName("Default")]
    [IsDependentOn(typeof(CleanSolutionTask))]
    [IsDependentOn(typeof(UpdateAssemblyInfoTask))]
    [IsDependentOn(typeof(TestAndUploadReportTask))]
    [IsDependentOn(typeof(PushTask))]
    public class DefaultTask : FrostingTask
    {
    }
}
