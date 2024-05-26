using Cake.Common;
using Cake.Core;
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
            var settings = new ProcessSettings()
               .UseWorkingDirectory(Constants.ResultsPath)
               .WithArguments(builder => builder
                   .Append(context.Tools.Resolve("dotnet-coverage.dll").FullPath)
                   .Append("merge")
                   .Append(System.IO.Path.Combine($"{context.CoberturaFolder.FullPath}", "**/*.coverage"))
                   .Append("-f xml")
                   .AppendSwitchQuoted("--output", ":", context.CoberturaResultFile.FullPath)
               );

            context.StartProcess("dotnet", settings);
        }

        public override bool ShouldRun(BuildContext context)
        {
            return base.ShouldRun(context);
        }
    }
}
