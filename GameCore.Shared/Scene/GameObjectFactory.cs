namespace GameCore.GameObjects
{
    public interface IGameObjectFactory
    {
        GameObject FromAsset(SceneAsset asset);
    }

    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IComponentFactory _componentFactory;
        public GameObjectFactory(IComponentFactory componentFactory)
        {
            _componentFactory = componentFactory;
        }

        public GameObject FromAsset(SceneAsset asset)
        {
            var gameObject = new GameObject(asset.Position);
            var component = _componentFactory.FromAsset(asset);
            if (component.IsNotNull())
                gameObject.AddComponent(component);
            return gameObject;
        }
    }
}