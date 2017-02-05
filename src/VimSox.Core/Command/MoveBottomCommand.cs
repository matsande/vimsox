using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveBottomCommand : ICommand
    {
        private ISolutionExplorerControl solutionExplorerControl;

        public MoveBottomCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            this.solutionExplorerControl.MoveBottom();
            return new ExecutionResult(context.Clear(), CommandState.Handled);
        }
    }
}
