using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class ClearStackCommand : ICommand
    {
        public ExecutionResult Execute(IContext context, Keys key)
        {
            return new ExecutionResult(context.Clear().With(mode: InputMode.Normal), CommandState.Handled);
        }
    }
}