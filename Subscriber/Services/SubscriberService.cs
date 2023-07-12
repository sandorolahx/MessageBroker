using Common.Models;
using Subscriber.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Subscriber.Services
{
    public class SubscriberService : ISubscriberService
    {
        public readonly HttpClient _httpClient;

        public SubscriberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AckMessages(int subscriptionId, IList<int> ackIds)
        {
            try
            {
                await _httpClient.PostAsJsonAsync($"{Config.BASE_URL}/subscription/{subscriptionId}/deliveryconfirm", ackIds);
            }
            catch
            {
                // nop
            }
        }

        public async Task<IList<Message>> GetMessages(int subscriptionId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Message>>($"{Config.BASE_URL}/subscription/{subscriptionId}/messages");
                return response ?? new List<Message>();
            }
            catch
            {
                return new List<Message>();
            }
        }

        public async Task<Topic> GetTopicById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Topic>($"{Config.BASE_URL}/topic/{id}");
        }

        public async Task<Subscription> Subscribe(int topicId, string name)
        {
            var response = await _httpClient.PostAsJsonAsync($"{Config.BASE_URL}/topic/{topicId}/subscribe", name);

            var jsonString = await response.Content.ReadAsStringAsync();

            var subscription = JsonSerializer.Deserialize<Subscription>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return subscription;
        }
    }
}
