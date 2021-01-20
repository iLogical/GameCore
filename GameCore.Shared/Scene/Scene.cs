using System;
using System.Collections.Generic;
using GameCore.Display;
using Sample16_CustomTextLayoutCommands.Assets;
using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using SpriteBatch = GameCore.Rendering.SpriteBatch;

namespace GameCore.GameObjects
{
    public interface IScene
    {
        SpriteBatch SpriteBatch { get; }
        Dictionary<SceneAsset, GameObject> Assets { get; }
    }

    public class Scene : IScene
    {
        public SpriteBatch SpriteBatch { get; }
        public Dictionary<SceneAsset, GameObject> Assets { get; }


        public Scene(IWindowManager windowManager)
        {
            SpriteBatch = new SpriteBatch();
            Assets = new Dictionary<SceneAsset, GameObject>
            {
                [SceneAsset.For(AssetType.Texture2D, "Logo", "desktop_uv256")] = new GameObject(windowManager.CurrentWindow.Center()),
                [SceneAsset.For(AssetType.SpriteFont, "PlaceholderText", GlobalFontID.SegoeUI.ToString())] = new(Vector2.Zero)
            };
        }
    }

    public class SceneAsset
    {
        public AssetType Type { get; }
        public string Name { get; }
        public string Resource { get; }

        public static SceneAsset For(AssetType type, string name, string resource)
        {
            return new(type, name, resource);
        }

        private SceneAsset(AssetType type, string name, string resource)
        {
            Type = type;
            Name = name;
            Resource = resource;
        }
    }
}