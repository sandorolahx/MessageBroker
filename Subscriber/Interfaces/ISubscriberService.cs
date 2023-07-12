using Common.Models;

namespace Subscriber.Interfaces
{
    public interface ISubscriberService
    {
        Task<Subscription> Subscribe(int topicId, string name);
        Task<IList<Message>> GetMessages(int subscriptionId);
        Task AckMessages(int subscriptionId, IList<int> ackIds);
        Task<Topic> GetTopicById(int id);
    }
}
