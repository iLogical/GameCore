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
        HashSet<SceneAsset> Assets { get; }
    }

    public class Scene : IScene
    {
        public SpriteBatch SpriteBatch { get; }
        public HashSet<SceneAsset> Assets { get; }


        public Scene(IWindowManager windowManager)
        {
            SpriteBatch = new SpriteBatch();
            Assets = new HashSet<SceneAsset>
            {
                SceneAsset.For("Logo", AssetType.Texture2D, "desktop_uv256", windowManager.CurrentWindow.Center()),
                SceneAsset.For("PlaceholderText", AssetType.SpriteFont, GlobalFontID.SegoeUI.ToString(), Vector2.Zero)
            };
        }
    }
}