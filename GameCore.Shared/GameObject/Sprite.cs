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

    public interface IComponent
    {
        void OnUpdate(UltravioletTime time);
        void OnRender(SpriteBatch spriteBatch, GameObject gameObject);
    }
}