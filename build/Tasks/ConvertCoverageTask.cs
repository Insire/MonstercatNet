using Cake.Common;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("ConvertCoverage")]
    [IsDependentOn(typeof(TestTask))]
    public sealed class ConvertCoverageTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            foreach (var file in context.GetFiles(System.IO.Path.Combine($"{context.CoberturaFolder.FullPath}", "**/*.coverage")))
            {
                var result = System.IO.Path.ChangeExtension(file.FullPath, ".xml");

                var settings = new ProcessSettings()
                    .UseWorkingDirectory(Constants.ResultsPath)
                    .WithArguments(builder => builder
                        .Append("analyze")
                        .AppendSwitchQuoted("-output", ":", result)
                        .Append(file.FullPath)
                    );

                context.StartProcess(context.Tools.Resolve("CodeCoverage.exe"), settings);
            }

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (context.Tools.Resolve("CodeCoverage.exe") is null)
            {
                context.Log.Information($"Skipped  {nameof(ConvertCoverageTask)}, since CodeCoverage.exe is not a registered tool");
                return false;
            }

            if (context.GetFiles(System.IO.Path.Combine(context.CoberturaFolder.FullPath, "**/*.coverage")).Count == 0)
            {
                context.Log.Information($"Skipped {nameof(PushLocallyTask)}, since there is no nupkg file in {context.CoberturaFolder.FullPath}");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
