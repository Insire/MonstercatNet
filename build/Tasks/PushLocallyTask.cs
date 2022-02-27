using Cake.Common;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("PushLocally")]
    [IsDependentOn(typeof(BuildAndPackTask))]
    public sealed class PushLocallyTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            foreach (var path in context.GetFiles(System.IO.Path.Combine(context.PackagesFolder.FullPath, "*.nupkg")))
            {
                var settings = new ProcessSettings()
                    .UseWorkingDirectory(".")
                    .WithArguments(builder => builder
                    .Append("push")
                    .AppendSwitchQuoted("-source", Constants.LocalNugetDirectory)
                    .AppendQuoted(path.FullPath));

                context.StartProcess(context.Tools.Resolve("nuget.exe"), settings);
            }

            base.Run(context);
        }

        public override bool ShouldRun(BuildContext context)
        {
            if (!context.BuildSystem().IsLocalBuild)
            {
                context.Log.Information($"Skipped {nameof(PushLocallyTask)}, since task is not running on a developer machine.");
                return false;
            }

            if (!context.DirectoryExists(Constants.LocalNugetDirectory))
            {
                context.Log.Information($"Skipped {nameof(PushLocallyTask)}, since there is no local directory ({Constants.LocalNugetDirectory}) to push nuget packages to.");
                return false;
            }

            if (!context.FileExists(context.Tools.Resolve("nuget.exe")))
            {
                context.Log.Information($"Skipped {nameof(PushLocallyTask)}, since there is no nuget.exe registered with cake");
                return false;
            }

            if (context.GetFiles(System.IO.Path.Combine(context.PackagesFolder.FullPath, "*.nupkg")).Count == 0)
            {
                context.Log.Information($"Skipped {nameof(PushLocallyTask)}, since there is no nupkg file in {context.PackagesFolder.FullPath}");
                return false;
            }

            return base.ShouldRun(context);
        }
    }
}
