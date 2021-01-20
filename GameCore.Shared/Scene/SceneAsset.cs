using Ultraviolet;

namespace GameCore.GameObjects
{
    public class SceneAsset
    {
        public AssetType Type { get; }
        public string Name { get; }
        public string Resource { get; }
        public Vector2 Position { get; }

        public static SceneAsset For(string name, AssetType type, string resource, Vector2 position)
        {
            return new(name, type, resource, position);
        }

        private SceneAsset(string name, AssetType type, string resource, Vector2 position)
        {
            Type = type;
            Name = name;
            Resource = resource;
            Position = position;
        }
    }
}