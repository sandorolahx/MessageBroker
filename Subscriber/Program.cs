using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Subscriber;
using Subscriber.Interfaces;

var container = Startup.ConfigureServices();
var _subscriberService = container.GetRequiredService<ISubscriberService>();

var topic = await _subscriberService.GetTopicById(Config.TEST_TOPIC_ID);

if (topic == null)
{
    Console.WriteLine($"Topic with id {Config.TEST_TOPIC_ID} not found");
    Console.ReadLine();
}
else
{
    Console.WriteLine($"Topic: {topic.Id} - {topic.Name}");
    var subscription = await Subscribe(topic.Id);
    Console.WriteLine($"Subscription: {subscription.Id} - {subscription.Name}");

    do
    {
        Console.WriteLine("Listening...");
        while (!Console.KeyAvailable)
        {
            var messages = await _subscriberService.GetMessages(subscription.Id);
            var ids = new List<int>();
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.TopicMessage}");
                ids.Add(message.Id);
            }
            await _subscriberService.AckMessages(subscription.Id, ids);

            Thread.Sleep(2000);
        }

    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

}

async Task<Subscription> Subscribe(int topicId)
{
    Console.WriteLine("Please enter the subscription name:");
    var subscriptionName = Convert.ToString(Console.ReadLine());
    return await _subscriberService.Subscribe(topicId, subscriptionName);
}
