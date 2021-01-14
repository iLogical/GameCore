using System.Collections.Generic;
using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace GameCore.GameObjects
{
    public class Sprite : IComponent
    {
        private readonly Texture2D _texture2D;
        private readonly Vector2 _origin;
        private readonly Rectangle? _sourceRectangle;
        
        public Sprite(Texture2D texture2D) : this(texture2D, texture2D.Center(), new Rectangle(0, 0, texture2D.Width, texture2D.Height))
        {
        }
        
        public Sprite(Texture2D texture2D, Vector2 origin, Rectangle? sourceRectangle = null)
        {
            _texture2D = texture2D;
            _origin = origin;
            _sourceRectangle = sourceRectangle;
        }

        public void OnUpdate(UltravioletTime time)
        {
            throw new System.NotImplementedException();
        }

        public void OnRender(SpriteBatch spriteBatch, GameObject gameObject)
        {
            spriteBatch.Draw(_texture2D, gameObject.Position, _sourceRectangle, Color.White, gameObject.RotationRadians, _origin, gameObject.Scale, SpriteEffects.None, 0f);
        }
    }

    public class GameObject
    {
        private readonly HashSet<IComponent> _components;
        public Vector2 Position { get; }
        public Radians RotationRadians { get; }
        public float Scale { get; }

        public GameObject() : this(Vector2.Zero)
        {
        }

        public GameObject(Vector2 position, float rotationDegrees = 0.0f, float scale = 1.0f)
        {
            Position = position;
            RotationRadians = Radians.FromDegrees(rotationDegrees);
            Scale = scale;
            _components = new HashSet<IComponent>();
        }
        
        public GameObject AddComponent(IComponent component)
        {
            _components.Add(component);
            return this;
        }

        public void OnUpdate(UltravioletTime time)
        {
            foreach (var component in _components)
                component.OnUpdate(time);
        }

        public void OnRender(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.OnRender(spriteBatch, this);
        }
    }

    public interface IComponent
    {
        void OnUpdate(UltravioletTime time);
        void OnRender(SpriteBatch spriteBatch, GameObject gameObject);
    }
}