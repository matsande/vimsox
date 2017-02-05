using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public class Context : IContext
    {
        public Context()
        {
            this.DeferredCommand = null;
            this.Mode = InputMode.Normal;
            this.Stack = new List<Keys>();
        }

        public InputMode Mode { get; }
        
        public IReadOnlyCollection<Keys> Stack { get; }

        public ICommand DeferredCommand { get; }

        public IContext Add(Keys key)
        {
            return this.With(stack: this.Stack.Concat(new[] { key }));
        }


        public IContext With(InputMode? mode = null, ICommand delayedCommand = null, IEnumerable<Keys> stack = null)
        {
            var changed =
                (mode != null && mode.Value != this.Mode) ||
                (delayedCommand != null && !object.ReferenceEquals(delayedCommand, this.DeferredCommand) ||
                (stack != null && !object.ReferenceEquals(stack, this.Stack)));

            return changed
                ? new Context(delayedCommand ?? this.DeferredCommand, mode ?? this.Mode, stack ?? this.Stack)
                : this;
        }

        public IContext Clear()
        {
            return new Context();
        }

        private Context(ICommand delayedCommand, InputMode mode, IEnumerable<Keys> stack)
        {
            this.Mode = mode;
            this.DeferredCommand = delayedCommand;
            this.Stack = stack.ToList();
        }
    }
}