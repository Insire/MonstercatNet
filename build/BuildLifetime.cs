using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.GitVersion;
using Cake.Core;
using Cake.Frosting;
using Cake.GitVersioning;

namespace Build
{
    public sealed class BuildLifetime : FrostingLifetime<BuildContext>
    {
        public override void Setup(BuildContext context)
        {
            var gitVersion = context.GitVersion();
            context.GitVersion = context.GitVersioningGetVersion();
            context.Branch = gitVersion.BranchName;
            context.Commit = gitVersion.Sha;

            context.IsPublicRelease = context.Branch == "master";

            context.Information("Branch: {0}", context.Branch);

            if (context.IsPublicRelease)
            {
                context.Information("Building a {0} release.", "public");
            }
            else
            {
                context.Information("Building a {0}release.", "pre-");
            }

            context.Information($"nuget.exe ({context.Tools.Resolve("nuget.exe")}) {(context.FileExists(context.Tools.Resolve("nuget.exe")) ? "was found" : "is missing")}.");
            context.Information($"dotnet.exe ({context.Tools.Resolve("dotnet.exe")}) {(context.FileExists(context.Tools.Resolve("dotnet.exe")) ? "was found" : "is missing")}.");
            context.Information($"CodeCoverage.exe ({context.Tools.Resolve("CodeCoverage.exe")}) {(context.FileExists(context.Tools.Resolve("CodeCoverage.exe")) ? "was found" : "is missing")}.");

            context.Information($"NUGETORG_APIKEY was{(string.IsNullOrEmpty(context.EnvironmentVariable("NUGETORG_APIKEY")) ? " not" : "")} set.");
            context.Information($"CODECOV_TOKEN was{(string.IsNullOrEmpty(context.EnvironmentVariable("CODECOV_TOKEN")) ? " not" : "")} set.");
            context.Information($"ApiCredentials__Email was{(string.IsNullOrEmpty(context.EnvironmentVariable("ApiCredentials__Email")) ? " not" : "")} set.");
            context.Information($"ApiCredentials__Password was{(string.IsNullOrEmpty(context.EnvironmentVariable("ApiCredentials__Password")) ? " not" : "")} set.");

            context.Information("reportsFolder: {0}", context.ReportsFolder.FullPath);
            context.Information("coberturaResultFile: {0}", context.CoberturaResultFile.FullPath);

            context.Information("dotnet tool: {0}", context.Tools.Resolve("dotnet.exe"));
        }

        public override void Teardown(BuildContext context, ITeardownContext info)
        {
        }
    }
}
