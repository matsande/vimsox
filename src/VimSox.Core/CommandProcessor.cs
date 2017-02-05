using System.Collections.Generic;
using System.Windows.Forms;
using VimSox.Core.Command;
using VimSox.Core.Hook;

namespace VimSox.Core
{
    public class CommandProcessor : IKeyDispatcherTarget
    {
        private Dictionary<CommandKey, ICommand> commands = new Dictionary<CommandKey, ICommand>();
        private IContext context;

        private readonly ILogger logger;
        private readonly ISolutionExplorerControl solutionExplorerControl;

        public CommandProcessor(ISolutionExplorerControl solutionExplorerControl, ILogger logger, IContext initialContext = null)
        {
            this.solutionExplorerControl = solutionExplorerControl;
            this.logger = logger;
            this.context = initialContext ?? new Context();
            this.InitializeCommands();
        }

        public IContext Context => this.context;

        public bool OnKey(Keys key)
        {
            if (key == Keys.Return)
            {
                // Note: This is temporary, should be handled as other commands if we want to implement support for '/' search for instance.
                this.context = this.context.Clear();
                return false;
            }

            var handledKey = false;
            if (this.context.DeferredCommand != null)
            {
                var result = this.context.DeferredCommand.Execute(this.context, key);
                this.context = result.Context;
                handledKey = result.State == CommandState.Handled;
            }

            if (!handledKey)
            {
                var commandKey = new CommandKey(this.context.Mode, key);
                if (this.commands.TryGetValue(commandKey, out ICommand command))
                {
                    var result = command.Execute(this.context, key);
                    this.context = result.Context;
                    handledKey = result.State == CommandState.Handled;
                }
            }

            this.logger.Log($"{key} handled = {handledKey}");
            return handledKey;
        }

        private void InitializeCommands()
        {
            commands.Add(new CommandKey(InputMode.Normal, Keys.H), new MoveLeftCommand(this.solutionExplorerControl));
            commands.Add(new CommandKey(InputMode.Normal, Keys.J), new MoveDownCommand(this.solutionExplorerControl));
            commands.Add(new CommandKey(InputMode.Normal, Keys.K), new MoveUpCommand(this.solutionExplorerControl));
            commands.Add(new CommandKey(InputMode.Normal, Keys.L), new MoveRightCommand(this.solutionExplorerControl));

            commands.Add(new CommandKey(InputMode.Normal, Keys.G), new MoveTopCommand(this.solutionExplorerControl));
            commands.Add(new CommandKey(InputMode.Normal, Keys.G | Keys.Shift), new MoveBottomCommand(this.solutionExplorerControl));

            commands.Add(new CommandKey(InputMode.Normal, Keys.I), new EnterInsertModeCommand(CommandState.Handled));
            commands.Add(new CommandKey(InputMode.Normal, Keys.F2), new EnterInsertModeCommand(CommandState.PassThrough));

            // Note: Consider adding an option regarding the behavior of Escape in Normal mode
            // e.g should it clear the stack and stay in Normal mode, or should it fall through to the default behavior? 
            // Let's stick with the least disruptive mode for now.
            //commands.Add(new CommandKey(InputMode.Normal, Keys.Escape), new ClearStackCommand());

            commands.Add(new CommandKey(InputMode.Insert, Keys.Escape), new ExitInsertModeCommand());
        }

    }
}