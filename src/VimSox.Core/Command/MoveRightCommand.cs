using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveRightCommand : ICommand
    {
        private readonly ISolutionExplorerControl solutionExplorerControl;

        public MoveRightCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            this.solutionExplorerControl.MoveRight();
            return new ExecutionResult(context.Clear(), CommandState.Handled);
        }
    }
}