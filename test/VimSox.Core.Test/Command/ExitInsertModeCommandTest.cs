using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test.Command
{
    public class ExitInsertModeCommandTest
    {
        [Fact]
        public void ShouldClearStackAndEnterNormalMode()
        {
            var command = new ExitInsertModeCommand();
            var context = new Context().With(mode: InputMode.Insert);

            var result = command.Execute(context, System.Windows.Forms.Keys.Escape);
            Assert.Equal(CommandState.Handled, result.State);
            Assert.Equal(InputMode.Normal, result.Context.Mode);
        }
    }
}
