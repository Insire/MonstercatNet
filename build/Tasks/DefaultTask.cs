using Cake.Common;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;
using System.Linq;

namespace Build
{
    [TaskName("Default")]
    [IsDependentOn(typeof(CleanSolutionTask))]
    [IsDependentOn(typeof(UpdateAssemblyInfoTask))]
    [IsDependentOn(typeof(TestAndUploadReportTask))]
    [IsDependentOn(typeof(PushTask))]
    public class DefaultTask : FrostingTask
    {
        public override void Run(ICakeContext context)
        {
            if (context.BuildSystem().IsLocalBuild)
            {
                foreach (var envVar in context.EnvironmentVariables().OrderBy(p => p.Key).Where(p=> p.Key.StartsWith("ApiCredentials")))
                {
                   context.Information("Key: '{0}' Value: '{1}'", envVar.Key, envVar.Value);
                }
            }

            base.Run(context);
        }
    }
}
