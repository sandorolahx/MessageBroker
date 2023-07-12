using Publisher.Data;
using Publisher.Services;

namespace Publisher.Test.Services
{
    public class TopicServiceTest
    {
        private readonly AppDataContext _context;

        public TopicServiceTest()
        {
            _context = ContextGenerator.Generate();
        }

        [Fact]
        public async void Should_Get_Default_Topic_By_Id()
        {
            await ContextGenerator.Seed(_context);
            var topicService = new TopicService(_context);

            var topic = await topicService.GetById(1);

            Assert.NotNull(topic);
        }

        [Fact]
        public async void Should_Create_Topic()
        {
            var testTopicName = "New test topic";

            var topicService = new TopicService(_context);

            var topic = await topicService.Create(testTopicName);

            Assert.NotNull(topic);
            Assert.Equal(testTopicName, topic.Name);
        }
    }
}
