using System;
using System.Diagnostics;

namespace VimSox.Core.Hook
{
    public class ConditionalKeyHook : IDisposable
    {
        private static NativeMethods.KeyboardProc proc = KeyboardProc;
        private static ConditionalKeyHook activeInstance = null;

        private readonly IHookCondition condition;
        private readonly IKeyDispatcher dispatcher;
        private IntPtr hookId = IntPtr.Zero;

        public ConditionalKeyHook(IHookCondition condition, IKeyDispatcher dispatcher)
        {
            if (ConditionalKeyHook.activeInstance != null)
            {
                ConditionalKeyHook.activeInstance.Dispose();
            }

            ConditionalKeyHook.activeInstance = this;
            this.condition = condition;
            this.dispatcher = dispatcher;
            this.hookId = InstallHook(proc);
            Debug.Print($"Hook installed {this.hookId}");
        }

        private static IntPtr InstallHook(NativeMethods.KeyboardProc keyboardProc)
        {
            return NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD, keyboardProc, IntPtr.Zero, NativeMethods.GetCurrentThreadId());
        }

        private static IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return activeInstance.InternalKeyboardProc(nCode, wParam, lParam);
        }

        private IntPtr InternalKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                //Debug.Print($"Hook {nCode}, {wParam}, {lParam}");
                return this.condition.Active && this.dispatcher.Dispatch(nCode, wParam, lParam)
                    ? (IntPtr)1
                    : NativeMethods.CallNextHookEx(this.hookId, nCode, wParam, lParam);
            }
            catch (Exception e)
            {
                Debug.Print($"Exception in KeyboardProc, removing hook. Exception = {e}");
                var result = NativeMethods.CallNextHookEx(this.hookId, nCode, wParam, lParam);
                this.RemoveHook();
                return result;
            }
        }

        private void RemoveHook()
        {
            if (this.hookId != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(this.hookId);
                this.hookId = IntPtr.Zero;
            }
        }
        #region IDisposable Support

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this.RemoveHook();
                disposedValue = true;
            }
        }

        private bool disposedValue = false;

        ~ConditionalKeyHook()
        {
            Dispose(false);
        }
        #endregion IDisposable Support
    }
}