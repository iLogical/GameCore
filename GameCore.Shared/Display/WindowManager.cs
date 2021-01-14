using GameCore.Platform;
using Ultraviolet.Platform;

namespace GameCore.Display
{
    public interface IWindowManager
    {
        IWindowInfo CurrentWindow { get; }
        IWindowInfo PrimaryWindow { get; }
    }

    public class WindowManager : IWindowManager
    {
        private readonly IPlatformManager _platformManager;
        public IWindowInfo CurrentWindow => new WindowInfo(_platformManager.Platform?.Windows.GetCurrent());
        public IWindowInfo PrimaryWindow => new WindowInfo(_platformManager.Platform?.Windows.GetPrimary());
        

        public WindowManager(IPlatformManager platformManager)
        {
            _platformManager = platformManager;
        }

        private class WindowInfo : IWindowInfo
        {
            private readonly IUltravioletWindow _ultravioletWindow;
            public int Width => _ultravioletWindow?.ClientSize.Width ?? 0;
            public int Height => _ultravioletWindow?.ClientSize.Height ?? 0;

            public WindowInfo(IUltravioletWindow window)
            {
                _ultravioletWindow = window;
            }
        }
    }

    public interface IWindowInfo
    {
        public int Width { get; }
        public int Height { get; }
    }
}