using System.Diagnostics;
using VimSox.Core;

namespace VimSox
{
    public class DebugLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.Print(message);
        }
    }
}
