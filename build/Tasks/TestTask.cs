using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build
{
    [TaskName("Test")]
    [IsDependentOn(typeof(CleanSolutionAgainTask))]
    public sealed class TestTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var testSettings = new DotNetCoreTestSettings
            {
                Framework = Constants.TargetFramework,
                Configuration = Constants.Configuration,
                NoBuild = false,
                NoRestore = false,
                HandleExitCode = (code) => { context.TestsAreFailing = code != 0;return true; },
                ArgumentCustomization = builder => builder
                    .Append("--nologo")
                    .AppendSwitchQuoted("--results-directory", context.CoberturaFolder.FullPath)
                    .Append("-p:DebugType=full") // required for opencover codecoverage and sourcelinking
                    .Append("-p:DebugSymbols=true") // required for opencover codecoverage
                    .AppendSwitchQuoted("--collect", ":", "\"\"Code Coverage\"\"")
                    .Append($"--logger:trx;LogFileName={System.IO.Path.Combine(context.ResultsFolder.FullPath, "vsTestResults.trx")}"),
            };

            context.DotNetTest(Constants.TestProjectPath, testSettings);

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (string.IsNullOrEmpty(context.Environment.GetEnvironmentVariable("ApiCredentials__Email")))
            {
                context.Log.Information($"Skipped {nameof(TestTask)}, since environment variable ApiCredentials__Email missing or empty.");
                return false;
            }

            if (string.IsNullOrEmpty(context.Environment.GetEnvironmentVariable("ApiCredentials__Password")))
            {
                context.Log.Information($"Skipped  {nameof(TestTask)}, since environment variable ApiCredentials__Password missing or empty.");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
