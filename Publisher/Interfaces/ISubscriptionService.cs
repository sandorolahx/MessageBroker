using Common.Models;

namespace Publisher.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IList<Message>> GetSubscriptionMessages(int subscriptionId);

        Task<string> DeliveryConfirm(int subscriptionId, int[] confs);
    }
}
