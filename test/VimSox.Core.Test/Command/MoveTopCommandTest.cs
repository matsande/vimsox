using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moq;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test.Command
{
    public class MoveTopCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveTopAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveTop());

            var command = new MoveTopCommand(slnControl.Object);
            IContext context = new Context();
            var result = command.Execute(context, Keys.G);

            Assert.Equal(command, result.Context.DeferredCommand);
            Assert.Equal(CommandState.Handled, result.State);

            context = result.Context;
            result = command.Execute(context, Keys.G);
            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
    }
}
