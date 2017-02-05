using EnvDTE;
using EnvDTE80;
using VimSox.Core;
using VimSox.Core.Hook;

namespace VimSox
{
    public class SolutionExplorerHookCondition : IHookCondition
    {
        private bool active = false;
        private readonly DTE2 dte;
        private readonly ILogger logger;

        public SolutionExplorerHookCondition(DTE2 dte, ILogger logger)
        {
            this.dte = dte;
            this.logger = logger;
            dte.Events.WindowEvents.WindowActivated += OnWindowActivated;
        }

        private void OnWindowActivated(Window GotFocus, Window LostFocus)
        {
            this.active = (GotFocus == this.dte.ToolWindows.SolutionExplorer.Parent);
            this.logger.Log($"Window activated = {this.active}");
        }

        public bool Active => this.active;
    }
}
