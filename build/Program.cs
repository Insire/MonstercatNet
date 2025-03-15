using Cake.Frosting;

namespace Build
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .InstallTool(new Uri("nuget:?package=GitVersion.Tool&version=6.1.0"))
                .InstallTool(new Uri("nuget:?package=nuget.commandline&version=6.13.2"))
                .InstallTool(new Uri("nuget:?package=CodecovUploader&version=0.8.0"))
                .InstallTool(new Uri("nuget:?package=NUnit.ConsoleRunner&version=3.19.2"))
                .InstallTool(new Uri("nuget:?package=ReportGenerator&version=5.4.4"))
                .InstallTool(new Uri("dotnet:?package=dotnet-coverage&version=17.14.2"))
                .UseContext<BuildContext>()
                .UseLifetime<BuildLifetime>()
                .UseWorkingDirectory("..")
                .Run(args);
        }
    }
}
