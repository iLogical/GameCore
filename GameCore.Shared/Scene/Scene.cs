using System.Collections.Generic;
using GameCore.Display;
using GameCore.Rendering;

namespace GameCore.GameObjects
{
    public interface IScene
    {
        SpriteBatch SpriteBatch { get; }
        Dictionary<string, GameObject> Assets { get; }
    }

    public class Scene : IScene
    {
        public SpriteBatch SpriteBatch { get; }
        public Dictionary<string, GameObject> Assets { get; }


        public Scene(IWindowManager windowManager)
        {
            SpriteBatch = new SpriteBatch();
            Assets = new Dictionary<string, GameObject>
            {
                ["desktop_uv256"] = new(windowManager.CurrentWindow.Center())
            };
        }
    }
}