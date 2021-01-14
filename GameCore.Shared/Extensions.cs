using GameCore.Display;
using Ultraviolet;
using Ultraviolet.Graphics;

namespace GameCore
{
    public static class Extensions
    {
        public static bool IsNull(this object o)
        {
            return o == null;
        }
        public static bool IsNotNull(this object o)
        {
            return o != null;
        }

        public static Vector2 Center(this IWindowInfo windowInfo)
        {
            return new Vector2(windowInfo.Width / 2f, windowInfo.Height / 2f);
        }

        public static Vector2 Center(this Texture2D texture2D)
        {
            return new Vector2(texture2D.Width / 2f, texture2D.Height / 2f);
        }
    }
}