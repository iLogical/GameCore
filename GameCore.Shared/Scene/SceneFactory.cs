using System;
using System.Collections.Generic;
using GameCore.Display;

namespace GameCore.GameObjects
{
    public interface ISceneFactory
    {
        IScene CreateScene(string name);
    }

    public class SceneFactory : ISceneFactory
    {
        private readonly Dictionary<string, Func<IScene>> _scenes;

        public SceneFactory(IWindowManager windowManager)
        {
            _scenes = new Dictionary<string, Func<IScene>>
            {
                ["Test"] = () => new Scene(windowManager)
            };
        }
        
        public IScene CreateScene(string name)
        {
            return _scenes[name]();
        }
    }
}