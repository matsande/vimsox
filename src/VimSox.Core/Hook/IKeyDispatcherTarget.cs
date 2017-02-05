namespace VimSox.Core.Hook
{
    using System.Windows.Forms;
    public interface IKeyDispatcherTarget
    {
        bool OnKey(Keys key);
    }
}