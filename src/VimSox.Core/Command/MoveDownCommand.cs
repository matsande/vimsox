using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveDownCommand : ICommand
    {
        private readonly ISolutionExplorerControl solutionExplorerControl;

        public MoveDownCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            this.solutionExplorerControl.MoveDown();
            return new ExecutionResult(context.Clear(), CommandState.Handled);
        }
    }
}