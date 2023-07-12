using Common.Models;
using Microsoft.EntityFrameworkCore;
using Publisher.Data;
using Publisher.Interfaces;

namespace Publisher.Services
{
    public class TopicService : ITopicService
    {
        public readonly AppDataContext _context;

        public TopicService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Topic> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Topic name is mandatory");
            }

            var newTopic = new Topic() { Name = name };
            _context.Topics.Add(newTopic);

            await _context.SaveChangesAsync();

            return newTopic;
        }

        public async Task<IList<Topic>> GetAll()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetById(int id)
        {
            var topic = await _context.Topics.SingleOrDefaultAsync(t => t.Id == id);
            if (topic == null)
            {
                throw new Exception($"No topic found with id: {id}");
            }

            return topic;
        }

        public async Task PublishMessage(int topicId, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException(nameof(msg), "Topic message is mandatory");
            }

            bool topic = await _context.Topics.AnyAsync(t => t.Id == topicId);
            if (!topic)
            {
                throw new Exception($"Topic with id {topicId} not found");
            }

            var subscriptions = _context.Subscriptions.Where(s => s.TopicId == topicId);

            if (!subscriptions.Any())
            {
                throw new Exception($"There are no subscriptions for this topic {topicId}");
            }

            foreach (var sub in subscriptions)
            {
                var message = new Message()
                {
                    TopicMessage = msg,
                    SubscriptionId = sub.Id,
                };
                await _context.Messages.AddAsync(message);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Subscription> Subscribe(int topicId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Subscription name is mandatory");
            }

            bool topic = await _context.Topics.AnyAsync(t => t.Id == topicId);
            if (!topic)
            {
                throw new Exception($"Topic with id {topicId} not found");
            }

            var subscription = new Subscription()
            {
                TopicId = topicId,
                Name = name
            };

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }
    }
}
