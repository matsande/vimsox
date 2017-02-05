using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moq;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test
{
    public class CommandProcessorTest
    {
        [Fact]
        public void ShouldBeAbleToCreateInstance()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            var logger = new Mock<ILogger>();
            var commandProc = new CommandProcessor(slnControl.Object, logger.Object);

            // Not really a valid assertion, just making sure .ctor does not throw.
            Assert.NotNull(commandProc);
        }

        [Fact]
        public void ShouldClearContextOnReturnKey()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            var logger = new Mock<ILogger>();
            var context = new Mock<IContext>();
            context.Setup(x => x.Clear());
            var commandProc = new CommandProcessor(slnControl.Object, logger.Object, context.Object);

            var result = commandProc.OnKey(Keys.Return);

            Assert.False(result);
            context.VerifyAll();
        }

        [Fact]
        public void ShouldExecuteDeferredCommandIfPresent()
        {
            var slnControl = new Mock<ISolutionExplorerControl>();
            var logger = new Mock<ILogger>();
            var context = new Mock<IContext>();
            var expectedKey = Keys.A;

            var deferredCommand = new Mock<ICommand>();
            deferredCommand.Setup(x => x.Execute(context.Object, expectedKey))
                .Returns(new ExecutionResult(context.Object, CommandState.Handled));

            context.Setup(x => x.DeferredCommand)
                .Returns(deferredCommand.Object);

            var commandProc = new CommandProcessor(slnControl.Object, logger.Object, context.Object);
            var result = commandProc.OnKey(expectedKey);

            Assert.True(result);
            deferredCommand.VerifyAll();
        }
    }
}
