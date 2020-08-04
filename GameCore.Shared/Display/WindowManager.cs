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
            public int Width { get; }
            public int Height { get; }

            public WindowInfo(IUltravioletWindow window)
            {
                Width = window.ClientSize.Width;
                Height = window.ClientSize.Height;
            }
        }
    }

    public interface IWindowInfo
    {
        public int Width { get; }
        public int Height { get; }
    }
}