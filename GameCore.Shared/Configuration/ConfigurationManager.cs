using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.FreeType2;
using Ultraviolet.OpenGL;

namespace GameCore.Configuration
{
    public interface IConfigurationManager
    { 
        OpenGLUltravioletContext UltravioletContext { get; }
        UltravioletContext BuildContext(UltravioletApplication ultravioletApplication);
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public OpenGLUltravioletContext UltravioletContext { get; private set; }
        private readonly OpenGLUltravioletConfiguration _configuration;

        public ConfigurationManager()
        {
            _configuration = new OpenGLUltravioletConfiguration();
            _configuration.Plugins.Add(new BASSAudioPlugin());
            _configuration.Plugins.Add(new FreeTypeFontPlugin());

#if DEBUG
            AddDebugConfig(_configuration);
#endif
        }

        public UltravioletContext BuildContext(UltravioletApplication ultravioletApplication)
        {
            return UltravioletContext = new OpenGLUltravioletContext(ultravioletApplication, _configuration);
        }

        private static void AddDebugConfig(UltravioletConfiguration configuration)
        {
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) => { System.Diagnostics.Debug.WriteLine(message); };
        }
    }
}