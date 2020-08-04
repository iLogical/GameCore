using GameCore.Configuration;
using GameCore.Display;
using GameCore.Input;
using GameCore.Platform;
using Microsoft.Extensions.DependencyInjection;

namespace GameCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            using var game = serviceProvider.GetService<IGame>();
            game.Run();
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConfigurationManager, ConfigurationManager>()
                .AddSingleton<IInputManager, InputManager>()
                .AddSingleton<IPlatformManager, PlatformManager>()
                .AddSingleton<IWindowManager, WindowManager>()
                .AddSingleton<IGame, Game>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}