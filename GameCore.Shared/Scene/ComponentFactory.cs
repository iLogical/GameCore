using GameCore.Content;
using Ultraviolet.Content;

namespace GameCore.GameObjects
{
    public interface IComponentFactory
    {
        IComponent FromAsset(SceneAsset asset);
    }

    public class ComponentFactory : IComponentFactory
    {
        private readonly IContentManager _contentManager;

        public ComponentFactory(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public IComponent FromAsset(SceneAsset asset)
        {
            return asset.Type switch
            {
                AssetType.Texture2D => new Sprite(asset.Name, _contentManager.GetTexture2D(asset.Resource)),
                AssetType.SpriteFont => new Text(asset.Name, _contentManager.LoadSpriteFont(AssetID.Parse(asset.Resource)), "Text"),
                _ => default
            };
        }
    }
}