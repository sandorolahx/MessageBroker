using Common.Models;
using Microsoft.EntityFrameworkCore;
using Publisher.Data;
using Publisher.Enums;
using Publisher.Interfaces;

namespace Publisher.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public readonly AppDataContext _context;

        public SubscriptionService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<string> DeliveryConfirm(int subscriptionId, int[] confs)
        {
            bool subscription = await _context.Subscriptions.AnyAsync(s => s.Id == subscriptionId);
            if (!subscription)
            {
                throw new Exception($"Subscription with id {subscriptionId} not found");
            }

            if (confs == null || confs.Length <= 0)
            {
                throw new Exception("Confs is mandatory");
            }

            int count = 0;
            foreach (int i in confs)
            {
                var msg = _context.Messages.FirstOrDefault(m => m.Id == i);

                if (msg != null)
                {
                    msg.MessageStatus = MessageStatusEnum.SENT;
                    count++;
                }
            }

            await _context.SaveChangesAsync();
            return $"Acknowledge {count}/{confs.Length} messages";

        }

        public async Task<IList<Message>> GetSubscriptionMessages(int subscriptionId)
        {
            bool subscription = await _context.Subscriptions.AnyAsync(s => s.Id == subscriptionId);
            if (!subscription)
            {
                throw new Exception($"Subscription with id {subscriptionId} not found");
            }

            var messages = _context.Messages.Where(m => m.SubscriptionId == subscriptionId
                            && m.MessageStatus != MessageStatusEnum.SENT
                            && DateTime.Now <= m.ExpiresAfter);
            if (!messages.Any())
            {
                throw new Exception($"No new messages");
            }

            foreach (var message in messages)
            {
                message.MessageStatus = MessageStatusEnum.REQUESTED;
            }

            return await messages.ToListAsync();
        }
    }
}
