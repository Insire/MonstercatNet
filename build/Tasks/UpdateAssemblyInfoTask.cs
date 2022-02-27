using Cake.Common.Solution.Project.Properties;
using Cake.Core.IO;
using Cake.Frosting;
using System;

namespace Build
{
    [TaskName("UpdateAssemblyInfo")]
    public sealed class UpdateAssemblyInfoTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            var assemblyInfoParseResult = context.ParseAssemblyInfo(Constants.AssemblyInfoPath);

            var settings = new AssemblyInfoSettings()
            {
                Product = assemblyInfoParseResult.Product,
                Company = assemblyInfoParseResult.Company,
                Trademark = assemblyInfoParseResult.Trademark,
                Copyright = $"© {DateTime.Today.Year} Insire",

                InternalsVisibleTo = assemblyInfoParseResult.InternalsVisibleTo,

                MetaDataAttributes = new[]
                {
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "Platform",
                        Value = Constants.Platform,
                    },
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "CompileDate",
                        Value = "[UTC]" + DateTime.UtcNow.ToString(),
                    },
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "PublicRelease",
                        Value = context.IsPublicRelease.ToString(),
                    },
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "Branch",
                        Value = context.Branch,
                    },
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "Commit",
                        Value = context.Commit,
                    },
                    new AssemblyInfoMetadataAttribute()
                    {
                        Key = "Version",
                        Value = context.GitVersion.SemVer2,
                    },
                }
            };

            context.CreateAssemblyInfo(new FilePath(Constants.AssemblyInfoPath), settings);

            base.Run(context);
        }
    }

}
