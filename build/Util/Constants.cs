namespace Build
{
    internal static class Constants
    {
        public const string Configuration = "Release";
        public const string Platform = "AnyCPU";
        public const string TargetFramework = "net70";

        public const string SolutionPath = "./MonstercatNet.sln";
        public const string AssemblyInfoPath = "./SharedAssemblyInfo.cs";

        public const string PackagesPath = "./tmp_build/packages";
        public const string ResultsPath = "./tmp_build/results";
        public const string ReportsPath = "./tmp_build/results/reports";
        public const string CoberturaPath = "./tmp_build/results/reports/cobertura";

        public const string LocalNugetDirectory = @"D:\Drop\NuGet";

        public const string ProjectPath = "./MonstercatNet/MonstercatNet.csproj";
        public const string TestProjectPath = "./MonstercatNet.Tests/MonstercatNet.Tests.csproj";
    }
}
