using Common.Models;

namespace Publisher.Interfaces
{
    public interface ITopicService
    {
        Task<Topic> GetById(int id);

        Task<Topic> Create(string name);

        Task<IList<Topic>> GetAll();

        Task PublishMessage(int topicId, string msg);

        Task<Subscription> Subscribe(int topicId, string name);
    }
}
