using Cake.Common.IO;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("CoberturaReport")]
    [IsDependentOn(typeof(ConvertCoverageTask))]
    public sealed class CoberturaReportTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var files = context.GetFiles(System.IO.Path.Combine(context.CoberturaFolder.FullPath, "**/*.xml"));
            context.MergeReports(files, ReportGeneratorReportType.Cobertura, "cobertura");

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (context.GetFiles(System.IO.Path.Combine(context.CoberturaFolder.FullPath, "**/*.xml")).Count == 0)
            {
                context.Log.Information($"Skipped {nameof(CoberturaReportTask)}, since there is no coverage xml file in {context.CoberturaFolder.FullPath}");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
