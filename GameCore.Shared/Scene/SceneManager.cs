using GameCore.Content;
using GameCore.Rendering;
using Sample16_CustomTextLayoutCommands.Assets;

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

        public SceneManager(IContentManager contentManager, IRenderer renderer)
        {
            _contentManager = contentManager;
            _renderer = renderer;
        }

        public void LoadScene(IScene scene)
        {
            _contentManager.Purge();
            foreach (var (asset, gameObject) in scene.Assets)
            {
                gameObject.AddComponent(new Sprite(_contentManager.GetTexture2D(asset)));
                _renderer.Add(scene.SpriteBatch, gameObject);
            }

            var spriteFont = _contentManager.LoadSpriteFont(GlobalFontID.SegoeUI);
            var textObject = new GameObject().AddComponent(new Text(spriteFont, "Text"));
            _renderer.Add(scene.SpriteBatch, textObject);
        }
    }
}