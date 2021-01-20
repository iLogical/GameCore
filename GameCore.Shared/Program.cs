using GameCore.Configuration;
using GameCore.Content;
using GameCore.Display;
using GameCore.GameObjects;
using GameCore.Input;
using GameCore.Platform;
using GameCore.Rendering;
using Microsoft.Extensions.DependencyInjection;
namespace GameCore
{
    public static class Program
    {
        public static void Main()
        {
            var serviceProvider = BuildServiceProvider();
            using var game = serviceProvider.GetService<IGame>();
            game?.Run();
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConfigurationManager, ConfigurationManager>()
                .AddSingleton<IInputManager, InputManager>()
                .AddSingleton<IPlatformManager, PlatformManager>()
                .AddSingleton<IWindowManager, WindowManager>()
                .AddSingleton<IContentManager, ContentManager>()
                .AddSingleton<IComponentFactory, ComponentFactory>()
                .AddSingleton<IRenderer, Renderer>()
                .AddSingleton<ISceneManager, SceneManager>()
                .AddSingleton<ISceneFactory, SceneFactory>()
                .AddSingleton<IGame, Game>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}