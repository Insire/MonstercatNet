using Cake.Codecov;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build
{
    [TaskName("UploadCodecovReport")]
    [IsDependentOn(typeof(CoberturaReportTask))]
    public sealed class UploadCodecovReportTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.Codecov(new[] { context.CoberturaResultFile.FullPath }, context.Environment.GetEnvironmentVariable("CODECOV_TOKEN"));

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (!context.FileExists(context.CoberturaResultFile))
            {
                context.Log.Information($"Skipped {nameof(UploadCodecovReportTask)}, since {context.CoberturaResultFile} wasn't created.");
                return false;
            }

            if (!context.BuildSystem().IsRunningOnAzurePipelines)
            {
                context.Log.Information($"Skipped {nameof(UploadCodecovReportTask)}, since task is not running on AzurePipelines (Hosted).");
                return false;
            }

            if (string.IsNullOrEmpty(context.Environment.GetEnvironmentVariable("CODECOV_TOKEN")))
            {
                context.Log.Information($"Skipped {nameof(UploadCodecovReportTask)}, since environment variable CODECOV_TOKEN missing or empty.");
                return false;
            }

            if (context.TestsAreFailing)
            {
                context.Log.Information($"Skipped {nameof(UploadCodecovReportTask)}, since we got failing tests.");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
