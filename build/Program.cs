using Cake.Frosting;
using System;

namespace Build
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .InstallTool(new Uri("nuget:?package=GitVersion.CommandLine&version=5.12.0"))
                .InstallTool(new Uri("nuget:?package=nuget.commandline&version=6.10.0"))

                .InstallTool(new Uri("nuget:?package=CodecovUploader&version=0.7.3"))
                .InstallTool(new Uri("nuget:?package=NUnit.ConsoleRunner&version=3.17.0"))
                .InstallTool(new Uri("nuget:?package=ReportGenerator&version=5.3.4"))
                .InstallTool(new Uri("nuget:?package=dotnet-coverage&version=17.11.0"))
                .UseContext<BuildContext>()
                .UseLifetime<BuildLifetime>()
                .UseWorkingDirectory("..")
                .Run(args);
        }
    }
}
