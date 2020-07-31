using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.FreeType2;
using Ultraviolet.OpenGL;

namespace GameCore
{
    public interface IConfigurationManager
    {
        OpenGLUltravioletConfiguration BuildConfiguration();
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public OpenGLUltravioletConfiguration BuildConfiguration()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            configuration.Plugins.Add(new BASSAudioPlugin());
            configuration.Plugins.Add(new FreeTypeFontPlugin());

#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) => { System.Diagnostics.Debug.WriteLine(message); };
#endif
            return configuration;
        }
    }
}