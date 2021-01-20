using ProtoBuf;
using Ultraviolet;

namespace GameCore.GameObjects
{
    [ProtoContract]
    public class SceneAsset
    {
        [ProtoMember(1)]
        public AssetType Type { get;  set;}
        
        [ProtoMember(2)]
        public string Name { get;  set;}
        
        [ProtoMember(3)]
        public string Resource { get; set; }
        
        [ProtoMember(4)]
        public Vector2 Position { get; set; }

        public static SceneAsset For(string name, AssetType type, string resource, Vector2 position)
        {
            return new(name, type, resource, position);
        }

        public SceneAsset()
        {
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