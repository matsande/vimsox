using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimSox.Core.Command;
using Xunit;

namespace VimSox.Core.Test.Command
{
    public class EnterInsertModeCommandTest
    {
        [Fact]
        public void ShouldClearStackAndSetModeToInsert()
        {
            var expectedState = CommandState.Handled;
            var command = new EnterInsertModeCommand(expectedState);
            var context = new Context().With(mode: InputMode.Normal);

            var result = command.Execute(context, System.Windows.Forms.Keys.I);
            Assert.Equal(expectedState, result.State);
            Assert.Equal(InputMode.Insert, result.Context.Mode);
        }
    }
}
