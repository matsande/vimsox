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
    public class MoveLeftCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveLeftAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveLeft());

            var command = new MoveLeftCommand(slnControl.Object);
            var context = new Context();
            var result = command.Execute(context, Keys.H);
            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
    }
}
