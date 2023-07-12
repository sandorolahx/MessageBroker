using Publisher.Enums;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TopicMessage { get; set; }

        public int SubscriptionId { get; set; }

        [Required]
        public DateTime ExpiresAfter { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        public MessageStatusEnum MessageStatus { get; set; } = MessageStatusEnum.NEW;
    }
}
