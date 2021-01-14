using System;
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
            return new(windowInfo.Width * .5f, windowInfo.Height * .5f);
        }

        public static Vector2 Center(this Texture2D texture2D)
        {
            return new(texture2D.Width * .5f, texture2D.Height * .5f);
        }
        
        public static OneTimeAction OneTimeOnly(this Action action)
        {
            return OneTimeAction.From(action);
        }
    }
}