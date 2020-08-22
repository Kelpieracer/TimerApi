using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models.Topics;
using WebApi.Services;
using WebApi.Helpers;

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
    }
}
