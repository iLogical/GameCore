using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.Content;
using Ultraviolet.FreeType2;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Input;
using Ultraviolet.OpenGL;

namespace GameCore
{
    public class Game : UltravioletApplication
    {
        public Game()
            : base("iLogical", "GameCore")
        { }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            configuration.Plugins.Add(new BASSAudioPlugin());
            configuration.Plugins.Add(new FreeTypeFontPlugin());

#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) =>
            {
                System.Diagnostics.Debug.WriteLine(message);
            };
#endif

            return new OpenGLUltravioletContext(this, configuration);
        }

        protected override void OnInitialized()
        {
            UsePlatformSpecificFileSource();
            base.OnInitialized();
        }

        protected override void OnLoadingContent()
        {
            _contentManager = ContentManager.Create("Content");
            _spriteBatch = SpriteBatch.Create();
            _texture = _contentManager.Load<Texture2D>("desktop_uv256");

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            if (Ultraviolet.GetInput().GetKeyboard().IsKeyPressed(Key.Escape))
            {
                Exit();
            }
            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            var window = Ultraviolet.GetPlatform().Windows.GetCurrent();
            var position = new Vector2(window.ClientSize.Width / 2f, window.ClientSize.Height / 2f);
            var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            _spriteBatch.Draw(_texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.OnDrawing(time);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contentManager?.Dispose();

                _spriteBatch?.Dispose();
            }
            base.Dispose(disposing);
        }

        private ContentManager _contentManager;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
    }
}
