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
        private readonly IComponentFactory _componentFactory;

        public SceneManager(IContentManager contentManager, IComponentFactory componentFactory, IRenderer renderer)
        {
            _contentManager = contentManager;
            _renderer = renderer;
            _componentFactory = componentFactory;
        }

        public void LoadScene(IScene scene)
        {
            _contentManager.Purge();
            foreach (var (asset, gameObject) in scene.Assets)
            {
                var component = _componentFactory.FromAsset(asset);
                if (component.IsNotNull())
                    gameObject.AddComponent(component);
                _renderer.Add(scene.SpriteBatch, gameObject);
            }
        }
    }
}