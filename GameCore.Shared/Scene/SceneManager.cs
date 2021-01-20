using GameCore.Content;
using GameCore.Rendering;

namespace GameCore.GameObjects
{
    public interface ISceneManager
    {
        void LoadScene(IScene scene);
    }

    public class SceneManager : ISceneManager
    {
        private readonly IContentManager _contentManager;
        private readonly IRenderer _renderer;
        private readonly IGameObjectFactory _gameObjectFactory;

        public SceneManager(IContentManager contentManager, IGameObjectFactory gameObjectFactory, IRenderer renderer)
        {
            _contentManager = contentManager;
            _renderer = renderer;
            _gameObjectFactory = gameObjectFactory;
        }

        public void LoadScene(IScene scene)
        {
            _contentManager.Purge();
            foreach (var asset in scene.Assets)
                _renderer.Add(scene.SpriteBatch, _gameObjectFactory.FromAsset(asset));
        }
    }
}