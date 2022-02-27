using Cake.Frosting;
using System;

namespace Build
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .InstallTool(new Uri("nuget:?package=GitVersion.CommandLine&version=5.8.2"))
                .InstallTool(new Uri("nuget:?package=nuget.commandline&version=6.0.0"))

                .InstallTool(new Uri("nuget:?package=Codecov&version=1.13.0"))
                .InstallTool(new Uri("nuget:?package=NUnit.ConsoleRunner&version=3.15.0"))
                .InstallTool(new Uri("nuget:?package=ReportGenerator&version=5.0.4"))
                .InstallTool(new Uri("nuget:?package=Microsoft.CodeCoverage&version=17.1.0"))
                .UseContext<BuildContext>()
                .UseLifetime<BuildLifetime>()
                .UseWorkingDirectory("..")
                .Run(args);
        }
    }
}