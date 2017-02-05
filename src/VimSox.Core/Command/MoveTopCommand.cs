using System;
using System.Linq;
using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class MoveTopCommand : ICommand
    {
        private ISolutionExplorerControl solutionExplorerControl;

        public MoveTopCommand(ISolutionExplorerControl solutionExplorerControl)
        {
            this.solutionExplorerControl = solutionExplorerControl;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            var state = CommandState.Handled;
            if (context.Stack.Count > 0 && context.Stack.Last() == Keys.G && key == Keys.G)
            {
                this.solutionExplorerControl.MoveTop();
                context = context.Clear();
            }
            else if (key == Keys.G)
            {
                context = context.Add(key).With(delayedCommand: this);
            }
            else
            {
                context = context.Clear();
                state = CommandState.Cleared;
            }

            return new ExecutionResult(context, state);
        }
    }
}