using System;
using GameCore.Configuration;
using GameCore.Content;
using GameCore.GameObjects;
using GameCore.Input;
using GameCore.Rendering;
using Ultraviolet;
using ContentManager = Ultraviolet.Content.ContentManager;

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
        private readonly IContentManager _contentManager;
        private readonly IRenderer _renderer;
        private readonly ISceneManager _sceneManager;
        private readonly ISceneFactory _sceneFactory;
        private OneTimeAction _onReady;

        public Game(IConfigurationManager configurationManager, IInputManager inputManager, IContentManager contentManager, IRenderer renderer, ISceneManager sceneManager, ISceneFactory sceneFactory) : base("iLogical",
            "GameCore")
        {
            _configurationManager = configurationManager;
            _inputManager = inputManager;
            _contentManager = contentManager;
            _renderer = renderer;
            _sceneManager = sceneManager;
            _sceneFactory = sceneFactory;
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
            _contentManager.Initialise(ContentManager.Create("Content"));
            _onReady = OneTimeAction.From(LoadScene);
            base.OnLoadingContent();
        }

        protected override void OnUpdating(UltravioletTime time)
        {
            _inputManager.OnInputAction(InputAction.Exit, Exit);

            base.OnUpdating(time);
        }

        private void LoadScene()
        {
            var scene = _sceneFactory.CreateScene("Test");
            _sceneManager.LoadScene(scene);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            _onReady.TryRun();
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