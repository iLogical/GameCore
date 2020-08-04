using System;
using GameCore.Configuration;
using GameCore.Display;
using GameCore.Input;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace GameCore
{
    public interface IGame : IDisposable
    {
        void Run();
    }

    public class Game : UltravioletApplication, IGame
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly IInputManager _inputManager;
        private readonly IWindowManager _windowManager;
        private ContentManager _contentManager;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;

        public Game(IConfigurationManager configurationManager, IInputManager inputManager, IWindowManager windowManager) : base("iLogical", "GameCore")
        {
            _configurationManager = configurationManager;
            _inputManager = inputManager;
            _windowManager = windowManager;
        }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            return _configurationManager.BuildContext(this);
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
            _inputManager.OnInputAction(InputAction.Exit, Exit);
            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            var position = new Vector2(_windowManager.CurrentWindow.Width / 2f, _windowManager.CurrentWindow.Height / 2f);
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
