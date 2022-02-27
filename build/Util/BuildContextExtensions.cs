using Cake.Common.IO;
using Cake.Common.Solution;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.IO;
using Cake.Incubator.Project;
using System.Linq;

namespace Build
{
    internal static class BuildContextExtensions
    {
        public static void MergeReports(this BuildContext context, FilePathCollection files, ReportGeneratorReportType type, string subFolder)
        {
            context.ReportGeneratorSettings.ReportTypes = new[]
            {
                type
            };

            context.ReportGenerator(files, context.ReportsFolder.Combine(subFolder), context.ReportGeneratorSettings);
        }

        public static void GenerateReport(this BuildContext context, FilePath inputFile, ReportGeneratorReportType type, string subFolder)
        {
            context.ReportGeneratorSettings.ReportTypes = new[]
            {
                type
            };

            context.ReportGenerator(inputFile, context.ReportsFolder.Combine(subFolder), context.ReportGeneratorSettings);
        }

        public static void Clean(this BuildContext context, bool cleanBin, bool cleanObj, bool cleanOutput, bool cleanMisc)
        {
            var solution = context.ParseSolution(Constants.SolutionPath);

            foreach (var project in solution.Projects)
            {
                // check solution items and exclude solution folders, since they are virtual
                if (project.Name == "Solution Items")
                    continue;

                var projectFile = project.Path; // FilePath
                if (cleanBin)
                {
                    var binFolder = projectFile.GetDirectory().Combine("bin");
                    if (context.DirectoryExists(binFolder))
                    {
                        context.CleanDirectory(binFolder);
                    }
                }

                if (cleanObj)
                {
                    var objFolder = projectFile.GetDirectory().Combine("obj");
                    if (context.DirectoryExists(objFolder))
                    {
                        context.CleanDirectory(objFolder);
                    }
                }

                if (cleanOutput)
                {
                    var customProject = context.ParseProject(project.Path, configuration: Constants.Configuration, platform: Constants.Platform);
                    foreach (var path in customProject.OutputPaths)
                    {
                        context.CleanDirectory(path.FullPath);
                    }
                }
            }

            if (cleanMisc)
            {
                var folders = new[]
                {
                    new DirectoryPath(Constants.PackagesPath),
                    new DirectoryPath(Constants.ResultsPath),
                    new DirectoryPath(Constants.ReportsPath),
                    new DirectoryPath(Constants.CoberturaPath),
                };

                foreach (var folder in folders)
                {
                    context.EnsureDirectoryExists(folder);
                    context.CleanDirectory(folder, (file) => !file.Path.Segments.Last().Contains(".gitignore"));
                }
            }
        }
    }
}
