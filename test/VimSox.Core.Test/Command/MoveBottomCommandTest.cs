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
    public class MoveBottomCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveBottomAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveBottom());

            var command = new MoveBottomCommand(slnControl.Object);
            var context = new Context();
            var result = command.Execute(context, Keys.G | Keys.Shift);

            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
       
    }
}
