using GameCore.Configuration;
using GameCore.Input;
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
                .AddSingleton<IGame, Game>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}