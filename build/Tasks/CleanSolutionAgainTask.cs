using Cake.Frosting;

namespace Build
{
    [TaskName("CleanSolutionAgain")]
    public sealed class CleanSolutionAgainTask : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.Clean(true, true, true, false);

            base.Run(context);
        }
    }
}
