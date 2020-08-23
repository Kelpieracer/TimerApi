using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Services;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicsController : BaseController
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicResponse>> Read(int id)
        {
            return Ok(await _topicService.Read(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TopicResponse>> Create(CreateTopicRequest model)
        {
            return Ok(await _topicService.Create(model, Account.Id));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<TopicResponse>> Update(UpdateTopicRequest model)
        {
            return Ok(await _topicService.Update(model, Account.Id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopicResponse>> Delete(int id)
        {
            return Ok(await _topicService.Delete(id, Account.Id));
        }
    }
}
