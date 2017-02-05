using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveUpCommand : ICommand
    {
        private readonly ISolutionExplorerControl solutionExplorerControl;

        public MoveUpCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            this.solutionExplorerControl.MoveUp();
            return new ExecutionResult(context.Clear(), CommandState.Handled);
        }
    }
}