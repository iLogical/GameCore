using System;
using GameCore.Configuration;
using GameCore.GameObjects;
using GameCore.Input;
using GameCore.Rendering;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics;
using Sprite = GameCore.GameObjects.Sprite;

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
        private readonly IRenderer _renderer;
        private ContentManager _contentManager;
        private SpriteBatch _spriteBatch;

        public Game(IConfigurationManager configurationManager, IInputManager inputManager, IRenderer renderer) : base("iLogical", "GameCore")
        {
            _configurationManager = configurationManager;
            _inputManager = inputManager;
            _renderer = renderer;
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
            _spriteBatch = new SpriteBatch();
            _renderer.Add(_spriteBatch);

            _renderer.Add(_spriteBatch, new GameObject(new Vector2(400, 300)).AddComponent(new Sprite(_contentManager.Load<Texture2D>("desktop_uv256"))));

            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            _inputManager.OnInputAction(InputAction.Exit, Exit);
            
            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            _renderer.Draw();
            base.OnDrawing(time);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contentManager?.Dispose();

                _renderer?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}