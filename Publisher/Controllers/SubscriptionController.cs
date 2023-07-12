using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Publisher.Interfaces;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        public readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Route("{id}/messages")]
        public async Task<ActionResult<IList<Message>>> GetMessages(int id)
        {
            return Ok(await _subscriptionService.GetSubscriptionMessages(id));
        }

        [HttpPost]
        [Route("{id}/deliveryconfirm")]
        public async Task<ActionResult<string>> DeliveryConfirm(int id, [FromBody] int[] confs)
        {
            return Ok(await _subscriptionService.DeliveryConfirm(id, confs));
        }
    }
}
