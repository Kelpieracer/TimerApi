using AutoMapper;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;

namespace WebApi.Services
{
    public interface ITopicService
    {
        public TopicResponse Create(CreateTopicRequest model, Account account);
        public void Delete(int id, Account account);
    }

    public class TopicService : ITopicService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public TopicService(
            DataContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public TopicResponse Create(CreateTopicRequest model, Account account)
        {
            var topic = new Topic { Name = model.Name, AccountId = account.Id };
            if (_context.Topics.Any(t => t.Name == topic.Name && t.AccountId == topic.AccountId))
                ErrorMessages.Throw(ErrorMessages.Code.Conflict);

            _context.Topics.Add(topic);
            _context.SaveChanges();
            return _mapper.Map<TopicResponse>(topic);
        }

        public TopicResponse Read(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);
            return _mapper.Map<TopicResponse>(topic);
        }

        public TopicResponse Update(CreateTopicRequest model, Account account)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.Id == model.Id && t.AccountId == account.Id);
            if (topic == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);
            _mapper.Map(model, topic);
            _context.SaveChanges();
            return _mapper.Map<TopicResponse>(topic);
        }

        public void Delete(int id, Account account)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);

            if (topic.AccountId != account.Id)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);

            _context.Topics.Remove(topic);
            _context.SaveChanges();
            return;
        }
    }
}
