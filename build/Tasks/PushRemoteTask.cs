using Cake.Common;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("PushRemote")]
    [IsDependentOn(typeof(BuildAndPackTask))]
    public sealed class PushRemoteTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            foreach (var path in context.GetFiles(System.IO.Path.Combine(context.PackagesFolder.FullPath, "*.nupkg")))
            {
                var settings = new ProcessSettings()
                    .UseWorkingDirectory(".")
                    .WithArguments(builder => builder
                        .Append("push")
                        .AppendQuoted(path.FullPath)
                        .AppendSwitchSecret("-apikey", context.EnvironmentVariable("NUGETORG_APIKEY"))
                        .AppendSwitchQuoted("-source", "https://api.nuget.org/v3/index.json")
                        .AppendSwitch("-Verbosity", "detailed")
                    );

                context.StartProcess(context.Tools.Resolve("nuget.exe"), settings);
            }

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (context.BuildSystem().IsRunningOnAzurePipelines)
            {
                context.Log.Information($"Skipped {nameof(PushRemoteTask)}, since task is not running on Azure.");
                return false;
            }

            if (string.IsNullOrEmpty(context.EnvironmentVariable("NUGETORG_APIKEY")))
            {
                context.Log.Information($"Skipped {nameof(PushRemoteTask)}, since environment variable NUGETORG_APIKEY missing or empty.");
                return false;
            }

            if (!context.FileExists(context.Tools.Resolve("nuget.exe")))
            {
                context.Log.Information($"Skipped {nameof(PushRemoteTask)}, since there is no nuget.exe registered with cake");
                return false;
            }

            if (context.GetFiles(System.IO.Path.Combine(context.PackagesFolder.FullPath, "*.nupkg")).Count == 0)
            {
                context.Log.Information($"Skipped {nameof(PushRemoteTask)}, since there is no nupkg file in {context.PackagesFolder.FullPath}");
                return false;
            }

            if (context.TestsAreFailing)
            {
                context.Log.Information($"Skipped {nameof(PushRemoteTask)}, since we got failing tests.");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
