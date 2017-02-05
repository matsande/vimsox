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
    public class MoveUpCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveUpAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveUp());

            var command = new MoveUpCommand(slnControl.Object);
            var context = new Context();
            var result = command.Execute(context, Keys.K);
            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
    }
}
