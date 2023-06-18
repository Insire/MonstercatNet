using Cake.Common;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build
{
    [TaskName("BuildAndPack")]
    [IsDependentOn(typeof(CleanSolutionAgainTask))]
    public sealed class BuildAndPackTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var settings = new ProcessSettings()
                .UseWorkingDirectory(".")
                .WithArguments(builder => builder
                    .Append("pack")
                    .AppendQuoted(Constants.ProjectPath)
                    .Append($"-c {Constants.Configuration}")
                    .Append($"--output \"{Constants.PackagesPath}\"")
                    .Append($"-p:PackageVersion={context.GitVersion?.SemVer2}")
                    .Append($"-p:PublicRelease={context.IsPublicRelease}") // Nerdbank.GitVersioning - omit git commit ID

                    // Creating symbol packages
                    .Append("-p:IncludeSymbols=true")
                    .Append("-p:SymbolPackageFormat=snupkg")

                    // enable source linking
                    .Append("-p:PublishRepositoryUrl=true")

                    // Deterministic Builds
                    .Append("-p:EmbedUntrackedSources=true")
                );

            context.StartProcess(context.Tools.Resolve("dotnet.exe"), settings);

            base.Run(context);
        }
    }
}
