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
    public class MoveRightCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveRightAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveRight());

            var command = new MoveRightCommand(slnControl.Object);
            var context = new Context();
            var result = command.Execute(context, Keys.L);
            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
    }
}
