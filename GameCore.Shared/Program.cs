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
                .AddTransient<IConfigurationManager, ConfigurationManager>()
                .AddTransient<IGame, Game>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}