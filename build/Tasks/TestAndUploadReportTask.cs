using Cake.Frosting;

namespace Build
{
    [TaskName("TestAndUploadReport")]
    [IsDependentOn(typeof(HtmlReportTask))]
    [IsDependentOn(typeof(UploadCodecovReportTask))]
    public sealed class TestAndUploadReportTask : FrostingTask<BuildContext>
    {
    }
}
