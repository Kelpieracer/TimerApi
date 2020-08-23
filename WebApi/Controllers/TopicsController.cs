﻿using AutoMapper;
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
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create(CreateTopicRequest model)
        {
            return Ok(_topicService.Create(model, Account));
        }

        [Authorize]
        [HttpDelete("{id: int}")]
        public IActionResult Delete(int id)
        {
            _topicService.Delete(id, Account);
            return Ok(new { message = "Topic deleted successfully" });
        }
    }
}
