using AutoMapper;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using System;

namespace WebApi.Services
{
    public interface ITopicService
    {
        public ServiceReply Create(CreateTopicRequest model, Account account);
        public ServiceReply Delete(int id, Account account);
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

            var topic = new Topic { Name = model.Name, AccountId = account.Id };
            if (_context.Topics.Any(t => t.Name == topic.Name && t.AccountId == topic.AccountId))
                return new ServiceReply { ServiceResult = ServiceResult.Conflict, item = "Item already exists." };

            _context.Topics.Add(topic);
            _context.SaveChanges();
            return new ServiceReply { ServiceResult = ServiceResult.Created, item = new CreateTopicRequest { Name = topic.Name, Id = topic.TopicId } };
        }

        public ServiceReply Delete(int id, Account account)
        {
            if (account == null)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized access." };

            var topic = _context.Topics.FirstOrDefault(t => t.TopicId == id);
            if (topic == null)
                return new ServiceReply { ServiceResult = ServiceResult.NotFound, item = "Item not found." };

            if (topic.AccountId != account.Id)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized to delete this item." };

            _context.Topics.Remove(topic);
            _context.SaveChanges();
            return new ServiceReply { ServiceResult = ServiceResult.NoContent, item = topic };
        }
    }
}
