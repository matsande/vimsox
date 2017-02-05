using System.Linq;
using System.Windows.Forms;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test.Command
{
    public class ClearStackCommandTest
    {
        [Fact]
        public void ShouldClearContextStackWhenExecuted()
        {
            IContext context = new Context();
            context = context.Add(Keys.A);

            var command = new ClearStackCommand();
            var result = command.Execute(context, Keys.B);

            Assert.False(result.Context.Stack.Any());
        }

        [Fact]
        public void ShouldReturnHandledCommandState()
        {
            IContext context = new Context();
            context = context.Add(Keys.A);

            var command = new ClearStackCommand();
            var result = command.Execute(context, Keys.B);

            Assert.Equal(CommandState.Handled, result.State);
        }
    }
}