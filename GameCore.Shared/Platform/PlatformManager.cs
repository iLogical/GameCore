using GameCore.Configuration;
using Ultraviolet;

namespace GameCore.Platform
{
    public interface IPlatformManager
    {
        IUltravioletPlatform Platform { get; }
    }
    
    public class PlatformManager : IPlatformManager
    {       
        public IUltravioletPlatform Platform => _configurationManager.UltravioletContext?.GetPlatform();

        private readonly IConfigurationManager _configurationManager;
        public PlatformManager(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }
    }
}