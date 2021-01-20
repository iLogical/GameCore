using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.Graphics.Graphics2D.Text;

namespace GameCore.GameObjects
{
    public class Text : IComponent
    {
        private readonly TextRenderer _textRenderer;
        private readonly TextLayoutSettings _settings;
        private readonly string _name;
        private readonly string _text;
        
        public Text(string name, UltravioletFont spriteFont, string text)
        {
            _name = name;
            _text = text;
            _textRenderer = new TextRenderer();
            _settings = new TextLayoutSettings(spriteFont, null, null, TextFlags.Standard);
        }
        
        public void OnUpdate(UltravioletTime time)
        {
            throw new System.NotImplementedException();
        }

        public void OnRender(SpriteBatch spriteBatch, GameObject gameObject)
        {
            _textRenderer.Draw(spriteBatch, _text, gameObject.Position, Color.White, _settings);
        }
    }
}