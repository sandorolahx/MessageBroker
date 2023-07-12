using Microsoft.Extensions.DependencyInjection;
using Subscriber.Interfaces;
using Subscriber.Services;

namespace Subscriber
{
    public class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var provider = new ServiceCollection()
                .AddSingleton<ISubscriberService, SubscriberService>()
                .AddHttpClient()
                .BuildServiceProvider();

            return provider;
        }
    }
}
