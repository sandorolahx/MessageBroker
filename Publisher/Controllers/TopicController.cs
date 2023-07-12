using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Publisher.Interfaces;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        public readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Topic>> Get(int id)
        {
            return Ok(await _topicService.GetById(id));
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<IList<Topic>>> GetAll()
        {
            return Ok(await _topicService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Topic>> Create(string name)
        {
            return await _topicService.Create(name);
        }

        [HttpPost]
        [Route("{id}/publish")]
        public async Task<ActionResult<Topic>> PublishMessage(int id, string message)
        {
            await _topicService.PublishMessage(id, message);
            return Ok("Message has been published");
        }

        [HttpPost]
        [Route("{id}/subscribe")]
        public async Task<ActionResult<Subscription>> Subscribe(int id, [FromBody] string name)
        {
            return Ok(await _topicService.Subscribe(id, name));
        }
    }
}
