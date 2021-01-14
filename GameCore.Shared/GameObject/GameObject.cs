using System.Collections.Generic;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace GameCore.GameObjects
{
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
}