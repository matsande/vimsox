using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moq;
using VimSox.Core.Hook;
using Xunit;

namespace VimSox.Core.Test.Hook
{
    public class KeyDispatcherTest
    {
        [Fact]
        public void ShouldReturnFalseForNonPreviewKey()
        {
            var target = new Mock<IKeyDispatcherTarget>();
            var dispatcher = new KeyDispatcher(target.Object);

            Assert.False(dispatcher.Dispatch(0, IntPtr.Zero, IntPtr.Zero));
        }

        [Fact]
        public void ShouldReturnFalseForNonKeyDown()
        {
            var target = new Mock<IKeyDispatcherTarget>();
            var dispatcher = new KeyDispatcher(target.Object);

            Assert.False(dispatcher.Dispatch(3, (IntPtr)0, (IntPtr)(0x01 << 31)));
        }

        [Theory]
        [InlineData(Keys.ShiftKey)]
        [InlineData(Keys.ControlKey)]
        [InlineData(Keys.Menu)]
        public void ShouldReturnFalseForModifierKeys(Keys key)
        {
            var target = new Mock<IKeyDispatcherTarget>();
            var dispatcher = new KeyDispatcher(target.Object);

            Assert.False(dispatcher.Dispatch(3, (IntPtr)key, IntPtr.Zero));
        }
    }
}
