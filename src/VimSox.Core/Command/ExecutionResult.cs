namespace VimSox.Core.Command
{

    public class ExecutionResult
    {
        public ExecutionResult(IContext context, CommandState state)
        {
            this.Context = context;
            this.State = state;
        }

        public IContext Context { get; }
        public CommandState State { get; }
    }
}