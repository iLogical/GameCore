using System;
using System.Collections.Generic;
using Ultraviolet.Graphics;

namespace GameCore.Content
{
    public interface IContentManager : IDisposable
    {
        void Initialise(Ultraviolet.Content.ContentManager create);
        Texture2D LoadTexture2D(string asset, bool persistant = false);
        Texture2D GetTexture2D(string asset, bool persistant = false);
        void Purge();
        void PurgeAll();
    }

    public class ContentManager : IContentManager
    {
        private Ultraviolet.Content.ContentManager _contentManager;
        private readonly Dictionary<string, Texture2D> _permanentCache;
        private readonly Dictionary<string, Texture2D> _temporaryCache;

        public ContentManager()
        {
            _permanentCache = new Dictionary<string, Texture2D>();
            _temporaryCache = new Dictionary<string, Texture2D>();
        }

        public void Initialise(Ultraviolet.Content.ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Texture2D LoadTexture2D(string asset, bool persistant = false)
        {
            var texture2D = _contentManager.Load<Texture2D>(asset);
            if (persistant)
                return _permanentCache[asset] = texture2D;
            return _temporaryCache[asset] = texture2D;
        }

        public Texture2D GetTexture2D(string asset, bool persistant = false)
        {
            return GetOrLoad(asset, persistant);
        }

        private Texture2D GetOrLoad(string asset, bool persistant = false)
        {
            if (persistant)
                return _permanentCache.TryGetValue(asset, out var persistantTexture2D) ? persistantTexture2D : LoadTexture2D(asset, true);
            return _temporaryCache.TryGetValue(asset, out var temporaryTexture2D) ? temporaryTexture2D : LoadTexture2D(asset);
        }

        public void Purge()
        {
            _temporaryCache.Clear();
        }

        public void PurgeAll()
        {
            _temporaryCache.Clear();
            _permanentCache.Clear();
        }

        public void Dispose()
        {
            _temporaryCache.Clear();
            _permanentCache.Clear();
            _contentManager?.Dispose();
        }
    }
}