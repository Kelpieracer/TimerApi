using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicsController : BaseController
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        private readonly ControllerReply reply = new ControllerReply();
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpPost("create")]
        public IActionResult Create(CreateTopicRequest model)
        {
            return reply.Get(_topicService.Create(model, Account));
        }

        // [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete(DeleteTopicRequest model)
        {
            return reply.Get(_topicService.Delete(model.Id, Account));
        }
    }
}
