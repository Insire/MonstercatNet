using Cake.Common.IO;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("HtmlReport")]
    [IsDependentOn(typeof(ConvertCoverageTask))]
    public sealed class HtmlReportTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var pattern = new GlobPattern(System.IO.Path.Combine(context.CoberturaFolder.FullPath, "**/*.xml"));
            var files = context.GetFiles(pattern);

            context.MergeReports(files, ReportGeneratorReportType.Html, "html");

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (context.GetFiles(System.IO.Path.Combine(context.CoberturaFolder.FullPath, "**/*.xml")).Count == 0)
            {
                context.Log.Information($"Skipped {nameof(CoberturaReportTask)}, since there is no testresult xml file in {context.CoberturaFolder.FullPath}");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
