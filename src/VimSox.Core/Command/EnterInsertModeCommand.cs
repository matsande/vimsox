using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class EnterInsertModeCommand : ICommand
    {
        private readonly CommandState handledState;

        public EnterInsertModeCommand(CommandState handledState)
        {
            this.handledState = handledState;
        }

        public ExecutionResult Execute(IContext context, Keys key)
        {
            return new ExecutionResult(context.Clear().With(mode: InputMode.Insert), this.handledState);
        }
    }
}