using System;
using Ultraviolet;
using Ultraviolet.BASS;
using Ultraviolet.Content;
using Ultraviolet.FreeType2;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Input;
using Ultraviolet.OpenGL;
using Ultraviolet.Platform;

namespace GameCore
{
    public interface IGame : IDisposable
    {
        void Run();
    }

    public class Game : UltravioletApplication, IGame
    {
        private readonly IConfigurationManager _configurationManager;
        private ContentManager _contentManager;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private KeyboardDevice KeyboardDevice => Ultraviolet.GetInput().GetKeyboard();
        private IUltravioletWindow Window => Ultraviolet.GetPlatform().Windows.GetCurrent();

        public Game(IConfigurationManager configurationManager) : base("iLogical", "GameCore")
        {
            _configurationManager = configurationManager;
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            return new OpenGLUltravioletContext(this, _configurationManager.BuildConfiguration());
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
            if (KeyboardDevice.IsKeyPressed(Key.Escape))
                Exit();
            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            var position = new Vector2(Window.ClientSize.Width / 2f, Window.ClientSize.Height / 2f);
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
    }
}
