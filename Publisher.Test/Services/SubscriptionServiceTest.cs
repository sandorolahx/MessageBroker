using Publisher.Data;
using Publisher.Services;

namespace Publisher.Test.Services
{
    public class SubscriptionServiceTest
    {
        private readonly AppDataContext _context;

        public SubscriptionServiceTest()
        {
            _context = ContextGenerator.Generate();
        }

        [Fact]
        public async void Should_Get_Subscription_Messages_From_Topic()
        {
            // seed default topic
            await ContextGenerator.Seed(_context);
            var topicService = new TopicService(_context);
            var subscriptionService = new SubscriptionService(_context);

            // get default topic
            var defaultTopic = await topicService.GetById(1);
            Assert.NotNull(defaultTopic);

            // create test subscription
            var subscription = await topicService.Subscribe(defaultTopic.Id, "Subscriber name");
            Assert.NotNull(subscription);

            // publish test messages
            await topicService.PublishMessage(defaultTopic.Id, "Message1");
            await topicService.PublishMessage(defaultTopic.Id, "Message2");

            // get messages
            var messages = await subscriptionService.GetSubscriptionMessages(subscription.Id);

            Assert.NotNull(messages);
            Assert.Equal(2, messages.Count);
        }
    }
}
