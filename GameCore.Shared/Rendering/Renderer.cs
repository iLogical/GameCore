using System;
using System.Collections.Generic;
using GameCore.Display;
using GameCore.GameObjects;

namespace GameCore.Rendering
{
    public interface IRenderer: IDisposable
    {
        Renderer Add(SpriteBatch spriteBatch);
        Renderer Add(SpriteBatch spriteBatch, GameObject gameObject);
        void Draw();
    }

    public class Renderer : IRenderer
    {
        private readonly IWindowManager _windowManager;
        private readonly Dictionary<SpriteBatch, List<GameObject>> _spriteBatches;

        public Renderer(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _spriteBatches = new Dictionary<SpriteBatch, List<GameObject>>();
        }

        public Renderer Add(SpriteBatch spriteBatch)
        {
            _spriteBatches.Add(spriteBatch, new List<GameObject>());
            return this;
        }
        public Renderer Add(SpriteBatch spriteBatch, GameObject gameObject)
        {
            if (!_spriteBatches.TryGetValue(spriteBatch, out _))
                Add(spriteBatch);
            _spriteBatches[spriteBatch].Add(gameObject);
            return this;
        }

        public void Draw()
        {
            foreach (var (spriteBatch, gameObjects) in _spriteBatches)
            {
                foreach (var gameObject in gameObjects)
                {
                    spriteBatch.DrawFrame(gameObject);
                }
            }
        }

        public void Dispose()
        {
            foreach (var (spriteBatch, _) in _spriteBatches)
            {
                spriteBatch?.Dispose();
            }
        }
    }
}