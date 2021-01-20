using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.FreeType2;
using Ultraviolet.OpenGL;
using Ultraviolet.SDL2;

namespace GameCore.Configuration
{
    public interface IConfigurationManager
    { 
        SDL2UltravioletContext UltravioletContext { get; }
        UltravioletContext BuildContext(UltravioletApplication ultravioletApplication);
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public SDL2UltravioletContext UltravioletContext { get; private set; }

        public ConfigurationManager()
        {
            SetConfiguration();
        }

        private static SDL2UltravioletConfiguration SetConfiguration()
        {
            var configuration = new SDL2UltravioletConfiguration
            {
                InitialWindowPosition = new Rectangle(Point2.Zero, new Size2(1280, 720)),
            };
            configuration.Plugins.Add(new OpenGLGraphicsPlugin());
            configuration.Plugins.Add(new BASSAudioPlugin());
            configuration.Plugins.Add(new FreeTypeFontPlugin());

#if DEBUG
            AddDebugConfig(configuration);
#endif
            return configuration;
        }

        public UltravioletContext BuildContext(UltravioletApplication ultravioletApplication)
        {
            return UltravioletContext = new SDL2UltravioletContext(ultravioletApplication, SetConfiguration());
        }

        private static void AddDebugConfig(UltravioletConfiguration configuration)
        {
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) => { System.Diagnostics.Debug.WriteLine(message); };
        }
    }
}