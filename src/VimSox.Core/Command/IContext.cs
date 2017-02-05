using System.Collections.Generic;
using System.Windows.Forms;

namespace VimSox.Core.Command
{
    public interface IContext
    {
        /// <summary>
        /// Gets the current deferred command if any.
        /// </summary>
        ICommand DeferredCommand { get; }

        /// <summary>
        /// Gets the current input mode.
        /// </summary>
        InputMode Mode { get; }

        /// <summary>
        /// Gets the current input stack.
        /// </summary>
        IReadOnlyCollection<Keys> Stack { get; }

        /// <summary>
        /// Adds a key the the input stack.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IContext Add(Keys key);

        /// <summary>
        /// Creates a new blank context. 
        /// </summary>
        /// <returns></returns>
        IContext Clear();

        /// <summary>
        /// Creates a new context with the non-null parameters updated. 
        /// </summary>
        /// <param name="mode">If specified, sets the value of <see cref="IContext.Mode"/></param>
        /// <param name="delayedCommand">If specified, sets the value of <see cref="IContext.DeferredCommand"/></param>
        /// <param name="stack">If specified, sets the value of <see cref="IContext.Stack"/></param>
        /// <returns>New context with the specified parameters updated.</returns>
        IContext With(
            InputMode? mode = null, 
            ICommand delayedCommand = null, 
            IEnumerable<Keys> stack = null);
    }
}