using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Services
{
    public interface ITopicService
    {
        public ServiceReply Create(CreateTopicRequest model, Account account);
    }

    public class TopicService : ITopicService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public TopicService(
            DataContext context
            // IMapper mapper,
            // IOptions<AppSettings> appSettings,
            // IEmailService emailService
            )
        {
            _context = context;
            // _mapper = mapper;
            // _appSettings = appSettings.Value;
            // _emailService = emailService;
        }

        public ServiceReply Create(CreateTopicRequest model, Account account)
        {
            if (account == null)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized access." };

            var topic = new Topic { Name = model.Name, Manager = account };
            if (_context.Topics.Any(t => t.Name == topic.Name && t.Manager.Id == topic.Manager.Id))
                return new ServiceReply { ServiceResult = ServiceResult.Conflict, item = "Item already exists." };


            _context.Topics.Add(topic);
            _context.SaveChanges();
            return new ServiceReply { ServiceResult = ServiceResult.Ok, item = topic };
        }
    }
}
