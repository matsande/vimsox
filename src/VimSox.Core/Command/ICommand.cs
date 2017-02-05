using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public interface ICommand
    {
        ExecutionResult Execute(IContext context, Keys key);
    }
}