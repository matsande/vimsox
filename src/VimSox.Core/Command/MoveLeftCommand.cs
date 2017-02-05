using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveLeftCommand : ICommand
    {
        private readonly ISolutionExplorerControl solutionExplorerControl;

        public MoveLeftCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            this.solutionExplorerControl.MoveLeft();
            return new ExecutionResult(context.Clear(), CommandState.Handled);
        }
    }
}