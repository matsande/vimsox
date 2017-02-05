namespace VimSox.Core.Hook
{
    using System;

    public interface IKeyDispatcher
    {
        bool Dispatch(int nCode, IntPtr wParam, IntPtr lParam);
    }
}