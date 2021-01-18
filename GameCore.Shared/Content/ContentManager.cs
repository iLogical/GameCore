using System;
using System.Collections.Generic;
using Sample16_CustomTextLayoutCommands.Assets;
using Ultraviolet.Content;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace GameCore.Content
{
    public interface IContentManager : IDisposable
    {
        void Initialise(Ultraviolet.Content.ContentManager create);
        Texture2D LoadTexture2D(string asset, bool persistant = false);
        Texture2D GetTexture2D(string asset, bool persistant = false);
        SpriteFont LoadSpriteFont(AssetID assetId, bool persistant = false);
        SpriteFont GetSpriteFont(AssetID assetId, bool persistant = false);
        void Purge();
        void PurgeAll();
    }

    public class ContentManager : IContentManager
    {
        private readonly Dictionary<string, Texture2D> _permanentTextureCache;
        private readonly Dictionary<string, Texture2D> _temporaryTextureCache;
        private readonly Dictionary<AssetID, SpriteFont> _permanentFontCache;
        private readonly Dictionary<AssetID, SpriteFont> _temporaryFontCache;
        private Ultraviolet.Content.ContentManager _contentManager;

        public ContentManager()
        {
            _permanentTextureCache = new Dictionary<string, Texture2D>();
            _temporaryTextureCache = new Dictionary<string, Texture2D>();
            _permanentFontCache = new Dictionary<AssetID, SpriteFont>();
            _temporaryFontCache = new Dictionary<AssetID, SpriteFont>();
        }

        public void Initialise(Ultraviolet.Content.ContentManager contentManager)
        {
            _contentManager = contentManager;
            LoadContentManifests(contentManager);
        }
        
        private static void LoadContentManifests(Ultraviolet.Content.ContentManager contentManager)
        {
            var contentManifestFiles = contentManager.GetAssetFilePathsInDirectory("Manifests");
            var contentManifestRegistry = contentManager.Ultraviolet.GetContent().Manifests;
            contentManifestRegistry.Load(contentManifestFiles);
            contentManifestRegistry["Global"]["Fonts"].PopulateAssetLibrary(typeof(GlobalFontID));
        }

        public Texture2D LoadTexture2D(string asset, bool persistant = false)
        {
            var texture2D = _contentManager.Load<Texture2D>(asset);
            if (persistant)
                return _permanentTextureCache[asset] = texture2D;
            return _temporaryTextureCache[asset] = texture2D;
        }

        public Texture2D GetTexture2D(string asset, bool persistant = false)
        {
            return GetOrLoad(asset, persistant);
        }

        private Texture2D GetOrLoad(string asset, bool persistant = false)
        {
            if (persistant)
                return _permanentTextureCache.TryGetValue(asset, out var persistantTexture2D) ? persistantTexture2D : LoadTexture2D(asset, true);
            return _temporaryTextureCache.TryGetValue(asset, out var temporaryTexture2D) ? temporaryTexture2D : LoadTexture2D(asset);
        }
        
        public SpriteFont LoadSpriteFont(AssetID assetId, bool persistant = false)
        {
            var spriteFont = _contentManager.Load<SpriteFont>(assetId);
            if (persistant)
                return _permanentFontCache[assetId] = spriteFont;
            return _temporaryFontCache[assetId] = spriteFont;
        }

        public SpriteFont GetSpriteFont(AssetID assetId, bool persistant = false)
        {
            return GetOrLoad(assetId, persistant);
        }

        private SpriteFont GetOrLoad(AssetID assetId, bool persistant = false)
        {
            if (persistant)
                return _permanentFontCache.TryGetValue(assetId, out var persistantTexture2D) ? persistantTexture2D : LoadSpriteFont(assetId, true);
            return _temporaryFontCache.TryGetValue(assetId, out var temporaryTexture2D) ? temporaryTexture2D : LoadSpriteFont(assetId);
        }

        public void Purge()
        {
            _temporaryTextureCache.Clear();
        }

        public void PurgeAll()
        {
            _temporaryTextureCache.Clear();
            _permanentTextureCache.Clear();
        }

        public void Dispose()
        {
            _temporaryTextureCache.Clear();
            _permanentTextureCache.Clear();
            _contentManager?.Dispose();
        }
    }
}