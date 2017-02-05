using System.Windows.Forms;
using Moq;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test.Command
{
    public class MoveDownCommandTest
    {
        [Fact]
        public void ShouldInvokeMoveDownAndSetStateToHandled()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            slnControl.Setup(x => x.MoveDown());

            var command = new MoveDownCommand(slnControl.Object);
            var context = new Context();
            var result = command.Execute(context, Keys.J);
            Assert.Equal(CommandState.Handled, result.State);
            slnControl.VerifyAll();
        }
    }
}
