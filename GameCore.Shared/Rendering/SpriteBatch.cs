using System;
using GameCore.GameObjects;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using UltravioletSpriteBatch = Ultraviolet.Graphics.Graphics2D.SpriteBatch;

namespace GameCore.Rendering
{
    public class SpriteBatch : IDisposable
    {
        private readonly UltravioletSpriteBatch _ultravioletSpriteBatch;

        public SpriteBatch()
        {
            _ultravioletSpriteBatch = UltravioletSpriteBatch.Create();
        }

        public void DrawFrame(GameObject gameObject)
        {
            _ultravioletSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            gameObject.OnRender(_ultravioletSpriteBatch);
            _ultravioletSpriteBatch.End();
        }

        public void Dispose()
        {
            _ultravioletSpriteBatch?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}